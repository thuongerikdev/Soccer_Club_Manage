import React from 'react';
import { useDrag } from 'react-dnd';



interface PlayerListProps {
  players: IPlayer[];
}

const PlayerItem: React.FC<{ player: IPlayer }> = ({ player }) => {
  const [{ isDragging }, drag] = useDrag({
    type: 'PLAYER',
    item: { 
      id: player.playerID, 
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
        #{player.playerID}
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
  var playerList = players.filter(x => x.playerStatus ==1)
  return (
    <div style={{ 
      maxHeight: '840px', // Chiều cao tối đa để kích hoạt thanh cuộn
      overflowY: 'auto',  // Kích hoạt thanh cuộn dọc
      margin: '10px 0',   // Thêm khoảng cách trên và dưới
      border: '1px solid #ddd', // Thêm viền cho danh sách
      borderRadius: '8px',
      // maxWidth:'300px' // Bo góc
    }}>
      <ul style={{ padding: 0, listStyleType: 'none' }}>
        {playerList.map((player, index) => (
          <li key={`${player.playerID}-${index}`}>
            <PlayerItem player={player} />
          </li>
        ))}
      </ul>
    </div>
  );
};

export default PlayerList;