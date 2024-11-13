import React from 'react';
import { useDrag } from 'react-dnd';

interface LineUp {
  lineUpId: number;
  lineUpName: string;
  clubId: number;
}

interface ListLineupProps {
  lineups: LineUp[];
  onSelectLineup: (lineupId: number) => void;
  selectedLineupId: number | null;
}

const LineupItem: React.FC<{ lineup: LineUp; onSelect: (lineupId: number) => void; isSelected: boolean }> = ({ lineup, onSelect, isSelected }) => {
  const [{ isDragging }, drag] = useDrag({
    type: 'LINEUP',
    item: { id: lineup.lineUpId, lineUpName: lineup.lineUpName },
    collect: (monitor) => ({
      isDragging: !!monitor.isDragging(),
    }),
  });

  const selectedStyle = isSelected
    ? { border: '2px solid #1976d2', background: '#e3f2fd', boxShadow: '0 4px 6px rgba(0, 0, 0, 0.1)' }
    : {};

  return (
    <div
      ref={drag}
      onClick={() => onSelect(lineup.lineUpId)}
      style={{
        padding: '15px',
        marginBottom: '12px',
        background: '#f9f9f9',
        border: '2px solid #2196F3',
        borderRadius: '8px',
        opacity: isDragging ? 0.6 : 1,
        cursor: 'pointer',
        ...selectedStyle,
      }}
    >
      <div style={{ fontSize: '18px', fontWeight: 'bold', color: '#2196F3' }}>
        {lineup.lineUpName}
      </div>
      <div style={{ fontSize: '14px', color: '#666' }}>
        LineUpID: {lineup.lineUpId} | Club ID: {lineup.clubId}
      </div>
    </div>
  );
};

const ListLineup: React.FC<ListLineupProps> = ({ lineups, onSelectLineup, selectedLineupId }) => {
  return (
    <div style={{ marginBottom: '20px', maxWidth: '300px' }}>
      {lineups.map(lineup => (
        <LineupItem
          key={lineup.lineUpId}
          lineup={lineup}
          onSelect={onSelectLineup}
          isSelected={lineup.lineUpId === selectedLineupId}
        />
      ))}
    </div>
  );
};

export default ListLineup;