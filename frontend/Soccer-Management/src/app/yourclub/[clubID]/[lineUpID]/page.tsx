"use client";

import React, { useState, useEffect } from "react";
import { DndProvider } from "react-dnd";
import { HTML5Backend } from "react-dnd-html5-backend";
import Lineup from "../../../../components/playerlineup/lineup";
import PlayerList from "../../../../components/playerlineup/playerlist";
import useSWR from "swr";
import withAuth from "@/components/middleware/withAuth";
import { useSelector } from "react-redux";
import { RootState } from "@/lib/store";
import { useRouter } from "next/navigation";
import { Container } from "react-bootstrap";
import DeleteModal from '../../../../components/modals/lineups/deleteLineup.modal';
import './lineup.scss';

const fetcher = (url: string) => fetch(url).then((res) => res.json());

type Params = {
  clubID: string;
  lineUpID: string;
};

type HomeProps = {
  params: Promise<Params>;
  userRole?: string | null;
};

const Home: React.FC<HomeProps> = ({ params, userRole }) => {
  const [clubID, setClubID] = useState<string | null>(null);
  const [lineupID, setLineupID] = useState<number | null>(null);
  const [selectedLineupId, setSelectedLineupId] = useState<number | null>(null);
  const [positions, setPositions] = useState<(IPlayer | null)[]>(Array(19).fill(null));
  const [lineupPlayers, setLineupPlayers] = useState<IPlayer[]>([]);
  const userId = useSelector((state: RootState) => state.auth.user?.userId);
  const [isOwner, setIsOwner] = useState<boolean>(false);
  const [loading, setLoading] = useState<boolean>(true);
  const [playerLineUpID, setPlayerLineUPID] = useState<number>(0);
  const [showModalDelete, setShowModalDelete] = useState<boolean>(false);

  // State to hold lineup ID for deletion
  const [lineUpId, setLineUpId] = useState<number>(0);

  const handleDelete = (id: number) => {
    setLineUpId(id);
    setShowModalDelete(true);
  };

  useEffect(() => {
    const fetchParams = async () => {
      const resolvedParams = await params;
      setClubID(resolvedParams.clubID);
      setLineupID(resolvedParams.lineUpID ? Number(resolvedParams.lineUpID) : null);
      setSelectedLineupId(lineupID);

      const userIdAsNumber = userId ? Number(userId) : undefined;
      await checkIfUserIsOwner(resolvedParams.clubID, userIdAsNumber);
    };

    fetchParams();
  }, [params, userId]);

  const checkIfUserIsOwner = async (clubID: string, userId?: number) => {
    try {
      const response = await fetch(`${process.env.NEXT_PUBLIC_CLUB}/get/${clubID}`);
      const clubOwnerData = await response.json();
      const clubOwnerUserID = clubOwnerData.data.userID;

      setIsOwner(clubOwnerUserID === userId);
    } catch (error) {
      console.error("Error checking ownership:", error);
    } finally {
      setLoading(false);
    }
  };

  const { data: playerData, error: playerError } = useSWR<{ data: IPlayer[] }>(
    clubID ? `${process.env.NEXT_PUBLIC_PLAYER}/getPlayerClub/${clubID}` : null,
    fetcher
  );

  const fetchLineupDetails = async (lineupId: number) => {
    try {
      const lineupResponse = await fetch(
        `${process.env.NEXT_PUBLIC_PLAYERLINEUP}/getPlayerLineUpByLineUP/${lineupId}`
      );

      if (!lineupResponse.ok) {
        const errorMessage = await lineupResponse.text();
        throw new Error(`Failed to fetch lineup details. Status: ${lineupResponse.status}, Response: ${errorMessage}`);
      }

      const lineupData = await lineupResponse.json();
      setPlayerLineUPID(lineupData.data.playerLineUpID);
      console.log(lineupData.data)

      if (lineupData.errorCode !== 0) {
        throw new Error(lineupData.errorMessage || "Unknown error occurred while fetching lineup");
      }

      const players: any[] = [];

      for (const lineupItem of lineupData.data) {
        try {
          const playerResponse = await fetch(`${process.env.NEXT_PUBLIC_PLAYER}/getPlayerById/${lineupItem.playerID}`);

          if (!playerResponse.ok) {
            const errorMessage = await playerResponse.text();
            console.warn(`Failed to fetch player ${lineupItem.playerID}. Status: ${playerResponse.status}, Response: ${errorMessage}`);
            players.push(null);
            continue;
          }

          const playerData = await playerResponse.json();
          players.push({
            ...playerData.data,
            playerPosition: lineupItem.position || playerData.data.playerPosition,
            isCaptain: lineupItem.isCaptain,
          });
        } catch (error) {
          console.error(`Error fetching player ${lineupItem.playerID}:`, error);
          players.push(null);
        }
      }

      const validPlayers = players.filter(Boolean);
      setLineupPlayers(validPlayers);

      const newPositions = Array(19).fill(null);
      validPlayers.forEach((player) => {
        if (player) {
          const positionIndex = parseInt(player.playerPosition) || 0;
          newPositions[positionIndex] = player;
        }
      });
      console.log(players)
      setPositions(newPositions);
    } catch (error) {
      console.error("Error fetching lineup details:", error);
      alert("Error fetching lineup details: " + (error || "Unknown error occurred"));
    }
  };

  const dropPlayer = (player: IPlayer, positionIndex: number) => {
    if (!isOwner) return;

    const existingIndex = positions.findIndex((pos) => pos?.playerID === player.playerID);
    if (existingIndex !== -1 && existingIndex !== positionIndex) {
      alert("This player is already assigned to another position.");
      return;
    }

    const newPositions = [...positions];
    newPositions[positionIndex] = player;
    setPositions(newPositions);
  };

  const removePlayerFromLineup = (positionIndex: number) => {
    if (isOwner) {
      const newPositions = [...positions];
      newPositions[positionIndex] = null;
      setPositions(newPositions);
    }
  };

  useEffect(() => {
   
      fetchLineupDetails(Number(lineupID));
    
  }, [lineupID]);

  if (loading) return <div className="loading">Loading...</div>;
  if (playerError) return <div>Error loading players.</div>;
  if (!playerData) return <div>Loading players...</div>;

  const displayedPlayers = selectedLineupId && lineupPlayers.length > 0 ? lineupPlayers : playerData.data;

  return (
    <Container className="new_container">
      <DndProvider backend={HTML5Backend}>
        <div style={{ display: "flex", padding: "20px" }}>
          <div style={{ flex: 1, marginRight: "20px" }}>
            <h1>Player List</h1>
            <PlayerList players={playerData.data} />
          </div>
          <div style={{ flex: 1 }}>
            <h1>Lineup</h1>
            {isOwner && (
              <button onClick={() => handleDelete(lineupID!)} className="btn btn-danger mb-3">
                Delete Lineup
              </button>
            )}
            
            <Lineup
              positions={positions}
              onDropPlayer={dropPlayer}
              onRemovePlayer={removePlayerFromLineup}
              selectedLineupId={Number(lineupID)}
              setPositions={setPositions}
              isOwner={isOwner}
              playerLineupID={playerLineUpID}
            />
          </div>
        </div>
      </DndProvider>
      {showModalDelete && lineUpId !== null && (
        <DeleteModal
          showModalDelete={showModalDelete}
          setShowModalDelete={setShowModalDelete}
          lineUpId={lineUpId} // lineUpId is guaranteed to be a number here
          setLineUpId={setLineUpId}
        />
      )}
    </Container>
  );
};

// Wrap the component with the HOC
export default withAuth(Home);