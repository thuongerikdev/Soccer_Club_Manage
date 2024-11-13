// Home.tsx
"use client"
import React, { useEffect, useState } from 'react';
import { DndProvider } from 'react-dnd';
import { HTML5Backend } from 'react-dnd-html5-backend';
import Lineup from '../../components/playerlineup/lineup';
import PlayerList from '../../components/playerlineup/playerlist';
import ListLineup from '../../components/playerlineup/savedlineup';
import useSWR from 'swr';
import withAuth from '@/components/middleware/withAuth';

const fetcher = (url: string) => fetch(url).then(res => res.json());

const Home: React.FC = () => {
  const { data: playerData, error: playerError } = useSWR<{ dt: IPlayer[] }>(
    'http://localhost:3001/api/players/getall',
    fetcher
  );

  const { data: lineupData, error: lineupError } = useSWR<{ data: LineUp[] }>(
    'http://localhost:3001/api/lineup/getall',
    fetcher
  );

  const [selectedLineupId, setSelectedLineupId] = useState<number | null>(null);
  const [positions, setPositions] = useState<(IPlayer | null)[]>(Array(19).fill(null)); // Assuming 19 positions
  const [lineupPlayers, setLineupPlayers] = useState<IPlayer[]>([]);

  const handleSelectLineup = async (lineupId: number) => {
    setSelectedLineupId(lineupId);
    await fetchLineupDetails(lineupId);
  };

  const fetchLineupDetails = async (lineupId: number) => {
    try {
      // Fetch lineup data
      const lineupResponse = await fetch(`${process.env.NEXT_PUBLIC_PLAYERLINEUP}/getbylineupid/${lineupId}`);
      if (!lineupResponse.ok) {
        throw new Error(`Failed to fetch lineup details. Status: ${lineupResponse.status}`);
      }
      
      const lineupData = await lineupResponse.json();
      if (lineupData.errorCode !== 0) {
        throw new Error(lineupData.errorMessage || 'Unknown error occurred while fetching lineup');
      }
  
      if (!Array.isArray(lineupData.data)) {
        throw new Error('Invalid lineup data format received');
      }
  
      // Fetch player details for each lineup item
      const players = await Promise.all(
        lineupData.data.map(async (lineupItem: any) => {
          const playerResponse = await fetch(`http://localhost:3001/api/players/getById/${lineupItem.playerId}`);
          
          if (!playerResponse.ok) {
            console.warn(`Failed to fetch player ${lineupItem.playerId}. Status: ${playerResponse.status}`);
            return null; // Allow missing players to be null and handle them in rendering logic
          }
          
          const playerData = await playerResponse.json();
          if (playerData.ec !== 0) {
            console.warn(`Error fetching player details: ${playerData.em}`);
            return null; // Handle API response error
          }
          
          return {
            ...playerData.dt, // Spread player data
            playerPosition: lineupItem.playerPosition || playerData.dt.playerPosition, // Use lineup or default position
            isCaptain: lineupItem.isCaptain, 
            playTime: lineupItem.playTime,
          };
        })
      );
  
      // Filter out any null players (in case some fetches failed)
      const validPlayers = players.filter(Boolean);
  
      setLineupPlayers(validPlayers);
  
      // Map player positions
      const newPositions = Array(19).fill(null);
      validPlayers.forEach((player) => {
        if (player) {
          const positionIndex = parseInt(player.playerPosition) || 0; // Default index for unknown positions
          newPositions[positionIndex] = player;
        }
      });
      setPositions(newPositions);
      
    } catch (error) {
      console.error('Error fetching lineup details:', error);
      alert('Error fetching lineup details: ' + error);
    }
  };
  

  const dropPlayer = (player: IPlayer, positionIndex: number) => {
    const newPositions = [...positions];
    newPositions[positionIndex] = player;
    setPositions(newPositions);
  };

  const removePlayerFromLineup = (positionIndex: number) => {
    const newPositions = [...positions];
    newPositions[positionIndex] = null;
    setPositions(newPositions);
  };

  if (playerError) return <div>Error loading players.</div>;
  if (!playerData) return <div>Loading players...</div>;

  if (lineupError) return <div>Error loading lineups.</div>;
  if (!lineupData || !lineupData.data || !Array.isArray(lineupData.data)) {
    return <div>No lineups available.</div>;
  }

  const displayedPlayers = selectedLineupId && lineupPlayers.length > 0 ? lineupPlayers : playerData.dt;
  return (
    <DndProvider backend={HTML5Backend}>
      <div style={{ display: 'flex', padding: '20px' }}>
        <div style={{ flex: 1, marginRight: '20px' }}>
          <div style={{ textAlign: 'center', marginBottom: '10px' }}>
            <h1>Player List</h1>
          </div>
          <PlayerList players={displayedPlayers} />
        </div>
        <div style={{ flex: 1 }}>
          <div style={{ textAlign: 'center', marginBottom: '10px' }}>
            <h1>Lineup</h1>
          </div>
          <Lineup
            positions={positions}
            onDropPlayer={dropPlayer}
            onRemovePlayer={removePlayerFromLineup}
            selectedLineupId={selectedLineupId}
            setPositions={setPositions}
          />
        </div>
        <div style={{ flex: 1, marginLeft: '20px' }}>
          <div style={{ textAlign: 'center', marginBottom: '10px' }}>
            <h1>LineUps</h1>
          </div>
          <ListLineup
            lineups={lineupData.data}
            onSelectLineup={handleSelectLineup}
            selectedLineupId={selectedLineupId}
          />
        </div>
      </div>
    </DndProvider>
  );
};

export default withAuth(Home) ;

// IPlayer, PlayerList, Lineup, ListLineup Components (as previously provided)
// Ensure each component is correctly fetching and displaying data

// Adjust any mapping or key assignments if your data format differs slightly
