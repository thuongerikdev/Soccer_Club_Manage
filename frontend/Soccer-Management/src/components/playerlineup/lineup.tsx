import React from 'react';
import { Button } from 'react-bootstrap';
import { useDrop } from 'react-dnd';
import './style.scss';

interface LineupProps {
  positions: (IPlayer | null)[];
  onDropPlayer: (player: IPlayer, positionIndex: number) => void;
  onRemovePlayer: (positionIndex: number) => void;
  selectedLineupId: number | null;
  setPositions: React.Dispatch<React.SetStateAction<(IPlayer | null)[]>>;
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
    <div ref={drop} className={`player-position ${player ? 'filled' : 'empty'}`}>
      {player ? (
        <div>
          {player.playerName}
          <button className="remove-button" onClick={() => onRemovePlayer(index)}>
            ×
          </button>
        </div>
      ) : (
        `Position ${index + 1}`
      )}
    </div>
  );
};

const Lineup: React.FC<LineupProps> = ({ positions, selectedLineupId, onDropPlayer, onRemovePlayer, setPositions }) => {
  const saveLineup = async () => {
    const payload = positions
      .map((player, index) => {
        if (player) {
          return {
            lineUpId: selectedLineupId,
            playerId: player.playerId,
            clubId: player.clubId,
            playerPosition: index.toString(), // Use index as player position
            isCaptain: false,
            playTime: 0,
          };
        }
        return null;
      })
      .filter((item) => item !== null);
  
    console.log("API URL:", `${process.env.NEXT_PUBLIC_PLAYERLINEUP}/create`);
    console.log("Payload:", payload);
  
    try {
      const response = await fetch(`${process.env.NEXT_PUBLIC_PLAYERLINEUP}/create`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload),
      });
  
      if (!response.ok) {
        const errorText = await response.text();
        console.error("Server response error:", errorText);
        throw new Error('Failed to save lineup');
      }
  
      alert('Lineup saved successfully!');
    } catch (error) {
      console.error(error);
      alert('Error saving lineup');
    }
  };
  
  return (
    <div className="lineup-container">
      {/* Top Row - 3 Positions */}
      <div style={{ display: 'grid', gridTemplateColumns: 'repeat(3, 1fr)', gap: '10px' }}>
        {[0, 1, 2].map((index) => (
          <PlayerPosition key={index} index={index} player={positions[index]} onDropPlayer={onDropPlayer} onRemovePlayer={onRemovePlayer} />
        ))}
      </div>

      {/* Second Row - 5 Positions */}
      <div style={{ display: 'grid', gridTemplateColumns: 'repeat(5, 1fr)', gap: '10px' }}>
        {[3, 4, 5, 6, 7].map((index) => (
          <PlayerPosition key={index} index={index} player={positions[index]} onDropPlayer={onDropPlayer} onRemovePlayer={onRemovePlayer} />
        ))}
      </div>

      {/* Third Row - 5 Positions */}
      <div style={{ display: 'grid', gridTemplateColumns: 'repeat(5, 1fr)', gap: '10px' }}>
        {[8, 9, 10, 11, 12].map((index) => (
          <PlayerPosition key={index} index={index} player={positions[index]} onDropPlayer={onDropPlayer} onRemovePlayer={onRemovePlayer} />
        ))}
      </div>

      {/* Fourth Row - 5 Positions */}
      <div style={{ display: 'grid', gridTemplateColumns: 'repeat(5, 1fr)', gap: '10px' }}>
        {[13, 14, 15, 16, 17].map((index) => (
          <PlayerPosition key={index} index={index} player={positions[index]} onDropPlayer={onDropPlayer} onRemovePlayer={onRemovePlayer} />
        ))}
      </div>

      {/* Bottom Row - 1 Position */}
      <PlayerPosition index={18} player={positions[18]} onDropPlayer={onDropPlayer} onRemovePlayer={onRemovePlayer} />

      <Button className="save-button" onClick={saveLineup}>
        Save Lineup
      </Button>
    </div>
  );
};

export default Lineup;