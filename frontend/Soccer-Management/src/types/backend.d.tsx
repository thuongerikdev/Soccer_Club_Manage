interface IBlog {
    id: number,
    content: string,
    author: string,
    title: string
}
interface IUser {
    userId: number,
    username: string,
    password: string,
    email: string,
    age: number,
    address: string,
    gender: string,
    phone: number,
    name: string
}
interface IClub {
    clubId: number;
    clubName: string;
    clubDescription: string;
    clubLogo: string;
    clubBanner: string;
    userId: number;
    budget: number;
    clubLevel: string;
    clubAge: string;
}

interface IPlayer {
    PlayerId: number;               // ID của cầu thủ
    PlayerName: string;             // Tên cầu thủ
    PlayerPosition: string;         // Vị trí cầu thủ
    PlayerNationality: string;      // Quốc tịch cầu thủ
    PlayerImage: string;            // URL của hình ảnh cầu thủ
    ClubId: number;                 // ID của câu lạc bộ mà cầu thủ thuộc về
    PlayerAge: number;              // Tuổi cầu thủ
    PlayerValue: number;            // Giá trị cầu thủ
    PlayerHealth: number;           // Sức khỏe cầu thủ
    PlayerSkill: number;            // Kỹ năng cầu thủ
    PlayerSalary: number;           // Lương cầu thủ
    Shirtnumber: number;            // Số áo cầu thủ
    PlayerStatus: number;           // Trạng thái cầu thủ (có thể là 0 cho không hoạt động, 1 cho hoạt động, v.v.)
    leg: string;                    // Chân thuận của cầu thủ (ví dụ: "right", "left")
    height: number;                 // Chiều cao cầu thủ (tính bằng mét hoặc cm)
    weight: number;                 // Cân nặng cầu thủ (tính bằng kg)
}
interface IPlayer {
    playerId: number;
    playerName: string;
    playerPosition: string;
    playerNationality: string;
    playerImage: string;
    playerAge: number;
    playerValue: number;
    playerHealth: number;
    playerSkill: number;
    playerSalary: number;
    shirtnumber: number;
    playerStatus: number;
    leg: string;
    height: number;
    weight: number;
}
interface Player {
    PlayerId: number;
    name: string;
    position: string;
    team: string;
    budget: number;
}
// types.ts or your relevant file
// types.ts or a similar file
 interface User {
    name: string;
    userId: string; // Assuming userId is a string as per your example
    exp: number;    // Assuming exp is a number (Unix timestamp)
  }