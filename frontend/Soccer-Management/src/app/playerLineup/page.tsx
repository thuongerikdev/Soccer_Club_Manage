// Home.tsx
'use client';
import React, { useState } from 'react';
import { DndProvider } from 'react-dnd';
import { HTML5Backend } from 'react-dnd-html5-backend';
import Lineup from '../../components/playerlineup/lineup';
import PlayerList from '../../components/playerlineup/playerlist';
import ListLineup from '../../components/playerlineup/savedlineup';
import useSWR from 'swr';

const fetcher = (url: string) => fetch(url).then(res => res.json());

const Home: React.FC = () => {
  const { data: playerData, error: playerError } = useSWR<{ dt: IPlayer[] }>(
    'http://localhost:3001/api/players/getall',
    fetcher,
    {
      revalidateIfStale: false,
      revalidateOnFocus: false,
      revalidateOnReconnect: false,
    }
  );

  const { data: lineupData, error: lineupError } = useSWR<{ data: LineUp[] }>(
    'http://localhost:3001/api/lineup/getall',
    fetcher,
    {
      revalidateIfStale: false,
      revalidateOnFocus: false,
      revalidateOnReconnect: false,
    }
  );
  const [selectedLineupId, setSelectedLineupId] = useState<number | null>(null);

  const handleSelectLineup = (lineupId: number) => {
    setSelectedLineupId(lineupId); // Set the selected lineup ID
  };

  const [positions, setPositions] = useState<(IPlayer | null)[]>(Array(4).fill(null));

  const dropPlayer = (player: IPlayer, positionIndex: number) => {
    const existingPositionIndex = positions.findIndex(p => p?.playerId === player.playerId);
    if (existingPositionIndex !== -1) {
      const updatedPositions = [...positions];
      updatedPositions[existingPositionIndex] = null;
      setPositions(updatedPositions);
    }

    const updatedPositions = [...positions];
    updatedPositions[positionIndex] = player;
    setPositions(updatedPositions);
  };

  const removePlayerFromLineup = (positionIndex: number) => {
    const updatedPositions = [...positions];
    updatedPositions[positionIndex] = null;
    setPositions(updatedPositions);
  };

  if (playerError) return <div>Error loading players.</div>;
  if (!playerData) return <div>Loading players...</div>;

  if (lineupError) return <div>Error loading lineups.</div>;
  if (!lineupData || !lineupData.data || !Array.isArray(lineupData.data)) {
    return <div>No lineups available.</div>;
  }

  return (
    <DndProvider backend={HTML5Backend}>
      <div style={{ display: 'flex', padding: '20px' }}>
        <div style={{ flex: 1, marginRight: '20px' }}>
          <div style={{ textAlign: 'center', marginBottom: '10px' }}>
            <h1>Player List</h1>
          </div>
          <PlayerList players={playerData.dt.sort((a, b) => b.playerId - a.playerId)} />
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
          />
        </div>
        <div style={{ flex: 1, marginLeft: '20px' }}>
          <div style={{ textAlign: 'center', marginBottom: '10px' }}>
            <h1>LineUps</h1>
          </div>
          <ListLineup
            lineups={lineupData.data}
            onSelectLineup={handleSelectLineup}
            selectedLineupId={selectedLineupId} // Pass the selectedLineupId here
          />
        </div>
      </div>
    </DndProvider>
  );
};

export default Home;
