import React from 'react';
import { useDrag } from 'react-dnd';

interface PlayerListProps {
  players: IPlayer[];
}

const PlayerItem: React.FC<{ player: IPlayer }> = ({ player }) => {
  const [{ isDragging }, drag] = useDrag({
    type: 'PLAYER',
    item: { 
      id: player.playerId, 
      playerName: player.playerName,
      playerPosition: player.playerPosition,
    },
    collect: (monitor) => ({
      isDragging: !!monitor.isDragging(),
    }),
  });

  return (
    <div ref={drag} style={{
      padding: '15px',
      marginBottom: '12px',
      background: '#f9f9f9',
      border: '2px solid #4caf50',
      borderRadius: '8px',
      boxShadow: '0 2px 6px rgba(0, 0, 0, 0.1)',
      opacity: isDragging ? 0.6 : 1,
      cursor: 'grab',
      transition: 'background 0.2s ease',
      fontFamily: 'Arial, sans-serif',
      display: 'flex',
      alignItems: 'center',
    }}>
      <div style={{
        marginRight: '12px',
        fontSize: '18px',
        fontWeight: 'bold',
        color: '#4caf50',
      }}>
        #{player.playerId}
      </div>
      <div style={{
        display: 'flex',
        flexDirection: 'column',
      }}>
        <span style={{
          fontSize: '16px',
          fontWeight: '600',
          color: '#333',
        }}>
          {player.playerName}
        </span>
        <span style={{
          fontSize: '14px',
          color: '#666',
        }}>
          Position: {player.playerPosition}
        </span>
      </div>
    </div>
  );
};

const PlayerList: React.FC<PlayerListProps> = ({ players }) => {
  return (
    <div style={{ marginBottom: '20px', maxWidth: '300px' }}>
      {players.map(player => (
        <PlayerItem key={player.playerId} player={player} />
      ))}
    </div>
  );
};

export default PlayerList;
