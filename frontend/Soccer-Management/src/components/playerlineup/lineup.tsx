import React from 'react';
import { Button } from 'react-bootstrap';
import { useDrop } from 'react-dnd';
import './style.scss';
import { useRouter, usePathname } from 'next/navigation'; 
interface LineupProps {
  positions: (IPlayer | null)[];
  onDropPlayer: (player: IPlayer, positionIndex: number) => void;
  onRemovePlayer: (positionIndex: number) => void;
  selectedLineupId: number;
  setPositions: React.Dispatch<React.SetStateAction<(IPlayer | null)[]>>;
  isOwner: boolean; // Nhận isOwner từ props
  playerLineupID : number
}

const PlayerPosition: React.FC<{
  index: number;
  player: IPlayer | null;
  onDropPlayer: (player: IPlayer, positionIndex: number) => void;
  onRemovePlayer: (positionIndex: number) => void;
  isOwner: boolean; // Thêm isOwner vào props
}> = ({ index, player, onDropPlayer, onRemovePlayer, isOwner }) => {
  const [, drop] = useDrop({
    accept: 'PLAYER',
    drop: (item: { id: number; playerName: string; playerPosition: string }) => {
      if (isOwner) {  // Chỉ cho phép kéo thả nếu là chủ sở hữu
        const droppedPlayer: IPlayer = {
          playerID: item.id,
          playerName: item.playerName,
          playerPosition: item.playerPosition,
          playerImage: '',
          clubId: 0,
          height: 0,
          leg: '',
          playerAge: 0,
          shirtnumber: 0,
          weight: 0,
          clubID: 0,
          phoneNumber: 0,
          playerStatus: 0
        };
        onDropPlayer(droppedPlayer, index);
      }
    },
    canDrop: () => !player && isOwner, // Không cho kéo thả nếu không phải chủ sở hữu
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

const Lineup: React.FC<LineupProps> = ({ positions, selectedLineupId, onDropPlayer, onRemovePlayer, setPositions, isOwner ,playerLineupID }) => {
  const router = useRouter();
  const saveLineup = async () => {
    console.log('Selected Lineup ID:', selectedLineupId); // Check this value
    const payload = positions
      .map((player, index) => {
        if (player) {
          return {
            lineUpID: selectedLineupId, // Use selectedLineupId from props
            playerID: player.playerID,
            status: "ok", // Adjust as necessary
            createdDate: new Date().toISOString(),
            position: index.toString(), // Ensure this is a string as expected
            isCaptain: false, // Change this based on your logic
          };
        }
        return null;
      })
      .filter((item) => item !== null);
  
    console.log("Payload:", payload);
  
    try {
      const response = await fetch(`${process.env.NEXT_PUBLIC_PLAYERLINEUP}/CreatePlayerLineUp`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload), // Send as an array
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
          <PlayerPosition
            key={index}
            index={index}
            player={positions[index]}
            onDropPlayer={onDropPlayer}
            onRemovePlayer={onRemovePlayer}
            isOwner={isOwner} // Truyền isOwner vào PlayerPosition
          />
        ))}
      </div>

      {/* Second Row - 5 Positions */}
      <div style={{ display: 'grid', gridTemplateColumns: 'repeat(5, 1fr)', gap: '10px' }}>
        {[3, 4, 5, 6, 7].map((index) => (
          <PlayerPosition
            key={index}
            index={index}
            player={positions[index]}
            onDropPlayer={onDropPlayer}
            onRemovePlayer={onRemovePlayer}
            isOwner={isOwner} // Truyền isOwner vào PlayerPosition
          />
        ))}
      </div>

      {/* Third Row - 5 Positions */}
      <div style={{ display: 'grid', gridTemplateColumns: 'repeat(5, 1fr)', gap: '10px' }}>
        {[8, 9, 10, 11, 12].map((index) => (
          <PlayerPosition
            key={index}
            index={index}
            player={positions[index]}
            onDropPlayer={onDropPlayer}
            onRemovePlayer={onRemovePlayer}
            isOwner={isOwner} // Truyền isOwner vào PlayerPosition
          />
        ))}
      </div>

      {/* Fourth Row - 5 Positions */}
      <div style={{ display: 'grid', gridTemplateColumns: 'repeat(5, 1fr)', gap: '10px' }}>
        {[13, 14, 15, 16, 17].map((index) => (
          <PlayerPosition
            key={index}
            index={index}
            player={positions[index]}
            onDropPlayer={onDropPlayer}
            onRemovePlayer={onRemovePlayer}
            isOwner={isOwner} // Truyền isOwner vào PlayerPosition
          />
        ))}
      </div>

      {/* Bottom Row - 1 Position */}
      <PlayerPosition
        index={18}
        player={positions[18]}
        onDropPlayer={onDropPlayer}
        onRemovePlayer={onRemovePlayer}
        isOwner={isOwner} // Truyền isOwner vào PlayerPosition
      />

      <Button className="save-button" onClick={saveLineup}>
        Save Lineup
      </Button>
    </div>
  );
};

export default Lineup;
