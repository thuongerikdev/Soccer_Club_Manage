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
    clubID: number;
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
    clubID: number;
    playerID: number;
    playerName: string;
    playerPosition: string;
    playerImage: string;
    clubId: number;
    height: number;
    leg: string;
    playerAge: number;
    shirtnumber: number;
    weight: number;
    phoneNumber : number,
    playerStatus :number
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
    lineUpID : number ;
    clubID : number ; 
    lineUpName : string ;
    lineUpType : string;

    createAt : string ;
    playerNumber : number
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
    matchesID : number ;
    matchesName : string;
    matchesDescription : string ;
    tournamentID : number ;
    stadium : string ;
    startTime : string ;
    endTime : string;
    teamWin : number ;
    teamLose : number,
    teamA : number ,
    teamB : number
}