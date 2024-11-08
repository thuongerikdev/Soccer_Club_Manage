import React from 'react';
import { useDrop } from 'react-dnd';

interface LineupProps {
  positions: (IPlayer | null)[];
  onDropPlayer: (player: IPlayer, positionIndex: number) => void;
  onRemovePlayer: (positionIndex: number) => void;
}

const PlayerPosition: React.FC<{
  index: number;
  player: IPlayer | null;
  onDropPlayer: (player: IPlayer, positionIndex: number) => void;
  onRemovePlayer: (positionIndex: number) => void;
}> = ({ index, player, onDropPlayer, onRemovePlayer }) => {
  const [, drop] = useDrop({
    accept: 'PLAYER',
    drop: (item: { id: number; playerName: string; playerPosition: string }) => {
      const droppedPlayer: IPlayer = {
        playerId: item.id,
        playerName: item.playerName,
        playerPosition: item.playerPosition,
        playerNationality: '',
        playerImage: '',
        clubId: 0,
        height: 0,
        leg: '',
        playerAge: 0,
        playerHealth: 0,
        playerSalary: 0,
        playerSkill: 0,
        playerStatus: 0,
        playerValue: 0,
        shirtNumber: 0,
        weight: 0,
        shirtnumber: 0,
      };
      onDropPlayer(droppedPlayer, index);
    },
    canDrop: () => !player,
  });

  return (
    <div ref={drop} style={{
      width: '100px',
      height: '100px',
      margin: '15px',
      border: '2px solid #ffffff',
      borderRadius: '50%',
      background: player ? '#ff6347' : '#ffffff', // Bright color for players
      color: player ? '#ffffff' : '#ff6347',
      textAlign: 'center',
      display: 'flex',
      justifyContent: 'center',
      alignItems: 'center',
      fontWeight: 'bold',
      fontSize: '14px',
      boxShadow: player ? '0 0 8px rgba(0, 0, 0, 0.2)' : 'none',
      position: 'relative',
      zIndex: 2,
    }}>
      {player ? (
        <div style={{ textAlign: 'center' }}>
          {player.playerName}
          <button
            style={{
              position: 'absolute',
              top: '5px',
              right: '5px',
              padding: '3px 6px',
              fontSize: '10px',
              color: '#fff',
              background: '#d32f2f',
              border: 'none',
              borderRadius: '3px',
              cursor: 'pointer',
            }}
            onClick={() => onRemovePlayer(index)}
          >
            ×
          </button>
        </div>
      ) : `Position ${index + 1}`}
    </div>
  );
};

const Lineup: React.FC<LineupProps> = ({ positions, onDropPlayer, onRemovePlayer }) => {
  return (
    <div style={{
      width: '800px',
      height: '1200px',
      background: 'linear-gradient(0deg, #4caf50 10%, #81c784 90%)', // Changed to green gradient for field
      display: 'grid',
      gridTemplateRows: 'repeat(5, auto)',
      justifyItems: 'center',
      gap: '10px',
      padding: '30px',
      borderRadius: '15px',
      border: '3px solid #ffffff',
      position: 'relative',
    }}>
      {/* Center Line */}
      <div style={{
        position: 'absolute',
        top: '50%',
        left: '0',
        right: '0',
        height: '2px',
        background: '#ffffff',
        zIndex: 0,
      }}></div>

      {/* Center Circle */}
      <div style={{
        position: 'absolute',
        top: '50%',
        left: '50%',
        width: '150px',
        height: '150px',
        marginTop: '-75px',
        marginLeft: '-75px',
        border: '2px solid #ffffff',
        borderRadius: '50%',
        zIndex: 0,
      }}></div>

      {/* Top Row - 3 Positions */}
      <div style={{
        display: 'grid',
        gridTemplateColumns: 'repeat(3, 1fr)',
        gridColumn: '1 / span 5',
        justifyItems: 'center',
      }}>
        <PlayerPosition index={0} player={positions[0]} onDropPlayer={onDropPlayer} onRemovePlayer={onRemovePlayer} />
        <PlayerPosition index={1} player={positions[1]} onDropPlayer={onDropPlayer} onRemovePlayer={onRemovePlayer} />
        <PlayerPosition index={2} player={positions[2]} onDropPlayer={onDropPlayer} onRemovePlayer={onRemovePlayer} />
      </div>

      {/* Second Row - 5 Positions */}
      <div style={{
        display: 'grid',
        gridTemplateColumns: 'repeat(5, 1fr)',
        gridColumn: '1 / span 5',
        justifyItems: 'center',
      }}>
        <PlayerPosition index={3} player={positions[3]} onDropPlayer={onDropPlayer} onRemovePlayer={onRemovePlayer} />
        <PlayerPosition index={4} player={positions[4]} onDropPlayer={onDropPlayer} onRemovePlayer={onRemovePlayer} />
        <PlayerPosition index={5} player={positions[5]} onDropPlayer={onDropPlayer} onRemovePlayer={onRemovePlayer} />
        <PlayerPosition index={6} player={positions[6]} onDropPlayer={onDropPlayer} onRemovePlayer={onRemovePlayer} />
        <PlayerPosition index={7} player={positions[7]} onDropPlayer={onDropPlayer} onRemovePlayer={onRemovePlayer} />
      </div>

      {/* Third Row - 5 Positions, centered on midline */}
      <div style={{
        display: 'grid',
        gridTemplateColumns: 'repeat(5, 1fr)',
        gridColumn: '1 / span 5',
        justifyItems: 'center',
        justifyContent: 'center', // Center the row
        marginTop: '-20px', // Align with center line
      }}>
        <PlayerPosition index={8} player={positions[8]} onDropPlayer={onDropPlayer} onRemovePlayer={onRemovePlayer} />
        <PlayerPosition index={9} player={positions[9]} onDropPlayer={onDropPlayer} onRemovePlayer={onRemovePlayer} />
        <PlayerPosition index={10} player={positions[10]} onDropPlayer={onDropPlayer} onRemovePlayer={onRemovePlayer} />
        <PlayerPosition index={11} player={positions[11]} onDropPlayer={onDropPlayer} onRemovePlayer={onRemovePlayer} />
        <PlayerPosition index={12} player={positions[12]} onDropPlayer={onDropPlayer} onRemovePlayer={onRemovePlayer} />
      </div>

      {/* Fourth Row - 5 Positions */}
      <div style={{
        display: 'grid',
        gridTemplateColumns: 'repeat(5, 1fr)',
        gridColumn: '1 / span 5',
        justifyItems: 'center',
      }}>
        <PlayerPosition index={13} player={positions[13]} onDropPlayer={onDropPlayer} onRemovePlayer={onRemovePlayer} />
        <PlayerPosition index={14} player={positions[14]} onDropPlayer={onDropPlayer} onRemovePlayer={onRemovePlayer} />
        <PlayerPosition index={15} player={positions[15]} onDropPlayer={onDropPlayer} onRemovePlayer={onRemovePlayer} />
        <PlayerPosition index={16} player={positions[16]} onDropPlayer={onDropPlayer} onRemovePlayer={onRemovePlayer} />
        <PlayerPosition index={17} player={positions[17]} onDropPlayer={onDropPlayer} onRemovePlayer={onRemovePlayer} />
      </div>

      {/* Bottom Row - 1 Position */}
      <div style={{ gridColumn: '3 / span 1' }}>
        <PlayerPosition
          index={18}
          player={positions[18]}
          onDropPlayer={onDropPlayer}
          onRemovePlayer={onRemovePlayer}
        />
      </div>
    </div>
  );
};

export default Lineup;
