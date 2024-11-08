'use client'
import React, { useState } from 'react';
import { DndProvider } from 'react-dnd';
import { HTML5Backend } from 'react-dnd-html5-backend';
import Lineup from '../../components/playerlineup/lineup';
import PlayerList from '../../components/playerlineup/playerlist';
import useSWR from 'swr';


const fetcher = (url: string) => fetch(url).then(res => res.json());

const Home: React.FC = () => {
  const { data, error } = useSWR<{ dt: IPlayer[] }>(`http://localhost:3001/api/players/getall`, fetcher, {
    revalidateIfStale: false,
    revalidateOnFocus: false,
    revalidateOnReconnect: false
  });

  const [positions, setPositions] = useState<(IPlayer | null)[]>(Array(4).fill(null)); // 4 vị trí

  const dropPlayer = (player: IPlayer, positionIndex: number) => {
    const existingPositionIndex = positions.findIndex(p => p?.playerId === player.playerId);
    
    // Nếu cầu thủ đã được đặt ở một vị trí khác, loại bỏ cầu thủ khỏi vị trí đó
    if (existingPositionIndex !== -1) {
      const updatedPositions = [...positions];
      updatedPositions[existingPositionIndex] = null; // Loại bỏ cầu thủ khỏi vị trí cũ
      setPositions(updatedPositions);
    }

    // Đặt cầu thủ vào vị trí mới
    const updatedPositions = [...positions];
    updatedPositions[positionIndex] = player;
    setPositions(updatedPositions);
  };

  const removePlayerFromLineup = (positionIndex: number) => {
    const updatedPositions = [...positions];
    updatedPositions[positionIndex] = null;
    setPositions(updatedPositions);
  };

  if (error) return <div>Error loading players.</div>;
  if (!data) return <div>Loading...</div>;

  return (
    <DndProvider backend={HTML5Backend}>
      <div style={{ display: 'flex', padding: '20px' }}>
        <div style={{ flex: 1, marginRight: '20px' }}>
          <h1>Player List</h1>
          <PlayerList players={data.dt.sort((a, b) => b.playerId - a.playerId)} />
        </div>
        <div style={{ flex: 1 }}>
          <h1>Lineup</h1>
          <Lineup
            positions={positions}
            onDropPlayer={dropPlayer}
            onRemovePlayer={removePlayerFromLineup}
          />
        </div>
      </div>
    </DndProvider>
  );
};

export default Home;