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
    playerId: number;
    playerName: string;
    playerPosition: string;
    playerNationality: string;
    playerImage: string;
    clubId: number;
    height: number;
    leg: string;
    playerAge: number;
    playerHealth: number;
    playerSalary: number;
    playerSkill: number;
    playerStatus: number;
    playerValue: number;
    shirtNumber: number;
    weight: number;
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