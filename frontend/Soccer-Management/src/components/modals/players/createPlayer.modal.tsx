import { useState } from 'react';
import { Form } from 'react-bootstrap';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import { toast } from 'react-toastify';
import { mutate } from 'swr';

interface IProps {
    showModalCreate: boolean;
    setShowModalCreate: (value: boolean) => void;
}

function CreatePlayerModal(props: IProps) {
    const { showModalCreate, setShowModalCreate } = props;

    // Khai báo trạng thái cho các thuộc tính của cầu thủ
    const [playerName, setPlayerName] = useState<string>('');
    const [playerPosition, setPlayerPosition] = useState<string>('');
    const [playerNationality, setPlayerNationality] = useState<string>('');
    const [playerImage, setPlayerImage] = useState<string>('');
    const [clubId, setClubId] = useState<number | ''>(0);
    const [playerAge, setPlayerAge] = useState<number | ''>(0);
    const [playerValue, setPlayerValue] = useState<number | ''>(0);
    const [playerHealth, setPlayerHealth] = useState<number | ''>(100);
    const [playerSkill, setPlayerSkill] = useState<number | ''>(50);
    const [playerSalary, setPlayerSalary] = useState<number | ''>(0);
    const [shirtNumber, setShirtNumber] = useState<number | ''>(0);
    const [playerStatus, setPlayerStatus] = useState<number | ''>(1);
    const [leg, setLeg] = useState<string>('');
    const [height, setHeight] = useState<number | ''>(0);
    const [weight, setWeight] = useState<number | ''>(0);

    const handleImageChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const file = event.target.files?.[0];
        if (file) {
            const reader = new FileReader();
            reader.onloadend = () => {
                setPlayerImage(reader.result as string); // Lưu chuỗi Base64 vào state
            };
            reader.readAsDataURL(file); // Đọc file và chuyển đổi thành Base64
        }
    };

    const handleSubmit = () => {
        fetch(`${process.env.NEXT_PUBLIC_PLAYER}/create`, {
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            method: "POST",
            body: JSON.stringify({
                PlayerName: playerName,
                PlayerPosition: playerPosition,
                PlayerNationality: playerNationality,
                PlayerImage: playerImage, // Sử dụng chuỗi Base64
                ClubId: clubId,
                PlayerAge: playerAge,
                PlayerValue: playerValue,
                PlayerHealth: playerHealth,
                PlayerSkill: playerSkill,
                PlayerSalary: playerSalary,
                Shirtnumber: shirtNumber,
                PlayerStatus: playerStatus,
                leg: leg,
                height: height,
                weight: weight,
            }),
        })
        .then(res => res.json())
        .then(res => {
            if (res) {
                toast.success("Create successful");
                handleCloseModal();
                mutate(`${process.env.NEXT_PUBLIC_PLAYER}/getall`);
            }
        })
        .catch(err => {
            toast.error("Error creating player");
            console.error(err);
        });
    };

    const handleCloseModal = () => {
        // Đặt lại các trường nhập liệu
        console.log(playerImage)
        setPlayerName('');
        setPlayerPosition('');
        setPlayerNationality('');
        setPlayerImage('');
        setClubId(0);
        setPlayerAge(0);
        setPlayerValue(0);
        setPlayerHealth(100);
        setPlayerSkill(50);
        setPlayerSalary(0);
        setShirtNumber(0);
        setPlayerStatus(1);
        setLeg('');
        setHeight(0);
        setWeight(0);
        setShowModalCreate(false);
    };

    return (
        <Modal
            show={showModalCreate}
            onHide={handleCloseModal}
            backdrop="static"
            keyboard={false}
            size='lg'
        >
            <Modal.Header closeButton>
                <Modal.Title>Create Player</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Form.Group className="mb-3">
                    <Form.Label>Player Name</Form.Label>
                    <Form.Control 
                        type="text" 
                        placeholder="Enter player name" 
                        value={playerName}
                        onChange={(e) => setPlayerName(e.target.value)} 
                    />
                </Form.Group>
                <Form.Group className="mb-3">
                    <Form.Label>Position</Form.Label>
                    <Form.Control 
                        type="text" 
                        placeholder="Enter player position" 
                        value={playerPosition}
                        onChange={(e) => setPlayerPosition(e.target.value)} 
                    />
                </Form.Group>
                <Form.Group className="mb-3">
                    <Form.Label>Nationality</Form.Label>
                    <Form.Control 
                        type="text" 
                        placeholder="Enter player nationality" 
                        value={playerNationality}
                        onChange={(e) => setPlayerNationality(e.target.value)} 
                    />
                </Form.Group>
                <Form.Group className="mb-3">
                    <Form.Label>Image</Form.Label>
                    <Form.Control 
                        type="file" 
                        accept="image/*" 
                        onChange={handleImageChange} 
                    />
                </Form.Group>
                <Form.Group className="mb-3">
                    <Form.Label>Club ID</Form.Label>
                    <Form.Control 
                        type="number" 
                        placeholder="Enter club ID" 
                        value={clubId === 0 ? '' : clubId}
                        onChange={(e) => setClubId(e.target.value ? Number(e.target.value) : 0)} 
                    />
                </Form.Group>
                <Form.Group className="mb-3">
                    <Form.Label>Age</Form.Label>
                    <Form.Control 
                        type="number" 
                        placeholder="Enter player age" 
                        value={playerAge === 0 ? '' : playerAge}
                        onChange={(e) => setPlayerAge(e.target.value ? Number(e.target.value) : 0)} 
                    />
                </Form.Group>
                <Form.Group className="mb-3">
                    <Form.Label>Value</Form.Label>
                    <Form.Control 
                        type="number" 
                        placeholder="Enter player value" 
                        value={playerValue === 0 ? '' : playerValue}
                        onChange={(e) => setPlayerValue(e.target.value ? Number(e.target.value) : 0)} 
                    />
                </Form.Group>
                <Form.Group className="mb-3">
                    <Form.Label>Health</Form.Label>
                    <Form.Control 
                        type="number" 
                        placeholder="Enter player health" 
                        value={playerHealth === 0 ? '' : playerHealth}
                        onChange={(e) => setPlayerHealth(e.target.value ? Number(e.target.value) : 100)} 
                    />
                </Form.Group>
                <Form.Group className="mb-3">
                    <Form.Label>Skill</Form.Label>
                    <Form.Control 
                        type="number" 
                        placeholder="Enter player skill" 
                        value={playerSkill === 0 ? '' : playerSkill}
                        onChange={(e) => setPlayerSkill(e.target.value ? Number(e.target.value) : 50)} 
                    />
                </Form.Group>
                <Form.Group className="mb-3">
                    <Form.Label>Salary</Form.Label>
                    <Form.Control 
                        type="number" 
                        placeholder="Enter player salary" 
                        value={playerSalary === 0 ? '' : playerSalary}
                        onChange={(e) => setPlayerSalary(e.target.value ? Number(e.target.value) : 0)} 
                    />
                </Form.Group>
                <Form.Group className="mb-3">
                    <Form.Label>Shirt Number</Form.Label>
                    <Form.Control 
                        type="number" 
                        placeholder="Enter shirt number" 
                        value={shirtNumber === 0 ? '' : shirtNumber}
                        onChange={(e) => setShirtNumber(e.target.value ? Number(e.target.value) : 0)} 
                    />
                </Form.Group>
                <Form.Group className="mb-3">
                    <Form.Label>Status</Form.Label>
                    <Form.Control 
                        type="number" 
                        placeholder="Enter player status" 
                        value={playerStatus === 0 ? '' : playerStatus}
                        onChange={(e) => setPlayerStatus(e.target.value ? Number(e.target.value) : 1)} 
                    />
                </Form.Group>
                <Form.Group className="mb-3">
                    <Form.Label>Leg</Form.Label>
                    <Form.Control 
                        type="text" 
                        placeholder="Enter leg (left/right)" 
                        value={leg}
                        onChange={(e) => setLeg(e.target.value)} 
                    />
                </Form.Group>
                <Form.Group className="mb-3">
                    <Form.Label>Height</Form.Label>
                    <Form.Control 
                        type="number" 
                        placeholder="Enter height in cm" 
                        value={height === 0 ? '' : height}
                        onChange={(e) => setHeight(e.target.value ? Number(e.target.value) : 0)} 
                    />
                </Form.Group>
                <Form.Group className="mb-3">
                    <Form.Label>Weight</Form.Label>
                    <Form.Control 
                        type="number" 
                        placeholder="Enter weight in kg" 
                        value={weight === 0 ? '' : weight}
                        onChange={(e) => setWeight(e.target.value ? Number(e.target.value) : 0)} 
                    />
                </Form.Group>
            </Modal.Body>
            <Modal.Footer>
                <Button variant="secondary" onClick={handleCloseModal}>
                    Close
                </Button>
                <Button variant="primary" onClick={handleSubmit}>Save</Button>
            </Modal.Footer>
        </Modal>
    );
}

export default CreatePlayerModal;