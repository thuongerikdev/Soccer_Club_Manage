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
    clubLevel: number;
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
interface Player {
  id: string;
  name: string;
}
// types.ts or your relevant file
// types.ts or a similar file
 interface User {
    name: string;
    userId: string; // Assuming userId is a string as per your example
    exp: number;    // Assuming exp is a number (Unix timestamp)
  }
interface LineUp {
    lineUpId : number ;
    clubId : number ; 
    matchId : number
    lineUpName : string ;
    lineUpType : string;
    matchType : string ;
    stadiumBackGround : string ;
    createAt : string ;
}
interface PlayerLineUp {
    playerLineUpId : number ;
    lineUpId : number ;
    playerId : number ;
    clubId : number ; 
    playerPosition : string ;
    isCaptain : boolean ;
    playTime : number
}
interface Matches {
    matchesId : number ;
    matchesName : string;
    matchesDescription : string ;
    tournamentId : number ;
    stadium : string ;
    startTime : string ;
    endTime : string;
    teamWin : number ;
    teamLose : number
}