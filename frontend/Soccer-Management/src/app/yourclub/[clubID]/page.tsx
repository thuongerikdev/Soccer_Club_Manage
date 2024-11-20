"use client";

import useSWR from "swr";
import { useState } from "react";
import { useParams } from "next/navigation";
import { useSelector } from "react-redux";
import { RootState } from "@/lib/store";
import MatchA from "../../../components/yourclub/matchesTeamA";
import MatchB from "../../../components/yourclub/matchesTeamB";
import Lineup from "../../../components/yourclub/clubLineUp";
import Player from "../../../components/yourclub/clubPlayer";
import "./clubstat.scss";
import DeleteModal from "../../../components/modals/clubs/deleteClub.modal";
// Fetcher function for swr
const fetcher = (url: string) => fetch(url).then((res) => res.json());

const YourClubStat = () => {
    const [clubId, setClubId] = useState<number>(0);
    const { clubID: paramClubID } = useParams<{ clubID?: string }>();
    const reduxUserID = useSelector((state: RootState) => state.auth.user?.userId);
    const clubID = paramClubID ? parseInt(paramClubID) : undefined;
    const [showModalDelete, setShowModalDelete] = useState<boolean>(false);
    // State quản lý component hiển thị
    const [activeTab, setActiveTab] = useState<string>("players");
    

    // Fetch club owner info
    const { data: clubOwnerData, error: clubOwnerError } = useSWR(
        clubID ? `${process.env.NEXT_PUBLIC_CLUB}/get/${clubID}` : null,
        fetcher
    );

    // Fetch players
    const { data: playersData, error: playersError } = useSWR(
        clubID ? `${process.env.NEXT_PUBLIC_PLAYER}/getPlayerClub/${clubID}` : null,
        fetcher
    );

    // Fetch lineup
    const { data: lineupData, error: lineupError } = useSWR(
        clubID ? `${process.env.NEXT_PUBLIC_LINEUP}/getLineUpClub/${clubID}` : null,
        fetcher
    );

    // Fetch matches if user is owner
    const isOwner = reduxUserID && clubOwnerData?.data?.userID === Number(reduxUserID);
    const { data: matchesAData, error: matchesAError } = useSWR(
        isOwner ? `${process.env.NEXT_PUBLIC_MATCHES}/TeamA/${clubID}` : null,
        fetcher
    );
    const { data: matchesBData, error: matchesBError } = useSWR(
        isOwner ? `${process.env.NEXT_PUBLIC_MATCHES}/TeamB/${clubID}` : null,
        fetcher
    );

    // Loading state
    if (!clubOwnerData || !playersData || !lineupData) {
        return <div className="loading">Loading...</div>;
    }

    // Error state
    if (clubOwnerError || playersError || lineupError) {
        return <div className="error">Failed to load data</div>;
    }
    
    const handleDelete = (id: number) => {
        setClubId(id);
        setShowModalDelete(true);
    };

    return (
        <div className="club-stat-container">
            <h2>Club Statistics</h2>
            <div className="button-group">
                <button
                    onClick={() => setActiveTab("players")}
                    className={activeTab === "players" ? "active" : ""}
                >
                    Players
                </button>
                <button
                    onClick={() => setActiveTab("lineup")}
                    className={activeTab === "lineup" ? "active" : ""}
                >
                    Lineup
                </button>
                {isOwner && (
                    <>
                        <button
                            onClick={() => setActiveTab("matchesA")}
                            className={activeTab === "matchesA" ? "active" : ""}
                        >
                            Matches A
                        </button>
                        <button
                            onClick={() => setActiveTab("matchesB")}
                            className={activeTab === "matchesB" ? "active" : ""}
                        >
                            Matches B
                        </button>
                    </>
                )}
            </div>
            <div className="club-stat-container">
                <h2>Club Statistics</h2>
                {isOwner && (
                    <button className="delete-club-button" onClick={() => handleDelete(Number(clubID))}>
                        Delete Club
                    </button>
                )}

                {/* Modal */}
                <DeleteModal
                    showModalDelete={showModalDelete}
                    setShowModalDelete={setShowModalDelete}
                    clubID={Number(clubID)}
                    setCLubId={setClubId}
                />
            </div>

            <div className="stats-content">
                {activeTab === "players" && <Player data={playersData.data} clubID={clubID} isOwner={isOwner} />}
                {activeTab === "lineup" && <Lineup data={lineupData.data} clubID={clubID} isOwner={isOwner} />}
                {isOwner && activeTab === "matchesA" && <MatchA data={matchesAData?.data} clubID={clubID} />}
                {isOwner && activeTab === "matchesB" && <MatchB data={matchesBData?.data} clubID={clubID} />}
            </div>
        </div>
    );
};

export default YourClubStat;
