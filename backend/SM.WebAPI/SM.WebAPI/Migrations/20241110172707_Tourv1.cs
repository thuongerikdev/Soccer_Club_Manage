using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SM.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class Tourv1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "tournament");

            migrationBuilder.CreateTable(
                name: "CelebrateEvent",
                schema: "tournament",
                columns: table => new
                {
                    EventID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Decor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Menu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Music = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    minigame = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ClubID = table.Column<int>(type: "int", nullable: false),
                    EventName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EventDescription = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EventDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EventLocation = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EventStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    membersCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CelebrateEvent", x => x.EventID);
                });

            migrationBuilder.CreateTable(
                name: "ClubPlayers",
                schema: "tournament",
                columns: table => new
                {
                    PlayerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClubID = table.Column<int>(type: "int", nullable: false),
                    PlayerName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PlayerPosition = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PlayerImage = table.Column<string>(type: "nvarchar(max)", maxLength: 5000000, nullable: false),
                    PhoneNumber = table.Column<int>(type: "int", nullable: false),
                    PlayerAge = table.Column<int>(type: "int", nullable: false),
                    Shirtnumber = table.Column<int>(type: "int", nullable: false),
                    PlayerStatus = table.Column<int>(type: "int", nullable: false),
                    leg = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    height = table.Column<double>(type: "float", nullable: false),
                    weight = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubPlayers", x => x.PlayerID);
                });

            migrationBuilder.CreateTable(
                name: "ClubTeam",
                schema: "tournament",
                columns: table => new
                {
                    ClubID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    ClubName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ClubDescription = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ClubLogo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ClubBanner = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Budget = table.Column<double>(type: "float", nullable: false),
                    ClubLevel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ClubAge = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubTeam", x => x.ClubID);
                });

            migrationBuilder.CreateTable(
                name: "LineUpBase",
                schema: "tournament",
                columns: table => new
                {
                    LineUpID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClubID = table.Column<int>(type: "int", nullable: false),
                    LineUpName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LineUpType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PlayerNumber = table.Column<int>(type: "int", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineUpBase", x => x.LineUpID);
                });

            migrationBuilder.CreateTable(
                name: "Minigames",
                schema: "tournament",
                columns: table => new
                {
                    MinigameID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TournamentID = table.Column<int>(type: "int", nullable: false),
                    MinigameType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDates = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDates = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Minigames", x => x.MinigameID);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                schema: "tournament",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    OrderStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderID);
                });

            migrationBuilder.CreateTable(
                name: "TeamMeetingEvent",
                schema: "tournament",
                columns: table => new
                {
                    EventID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MeetingAim = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MeetingContent = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ClubID = table.Column<int>(type: "int", nullable: false),
                    EventName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EventDescription = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EventDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EventLocation = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EventStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    membersCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamMeetingEvent", x => x.EventID);
                });

            migrationBuilder.CreateTable(
                name: "TournamentBase",
                schema: "tournament",
                columns: table => new
                {
                    TournamentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TournamentName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TournamentPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TournamentDescription = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TournamentType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TournamentStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TournamentLocation = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TournamentOrganizer = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TournamentContact = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentBase", x => x.TournamentID);
                });

            migrationBuilder.CreateTable(
                name: "TrainingEvent",
                schema: "tournament",
                columns: table => new
                {
                    EventID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainingAim = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LessonTraining = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Coach = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProcessResult = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ClubID = table.Column<int>(type: "int", nullable: false),
                    EventName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EventDescription = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EventDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EventLocation = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EventStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    membersCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingEvent", x => x.EventID);
                });

            migrationBuilder.CreateTable(
                name: "PlayerFund",
                schema: "tournament",
                columns: table => new
                {
                    PlayerFundID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerID = table.Column<int>(type: "int", nullable: false),
                    ClubID = table.Column<int>(type: "int", nullable: false),
                    FundActionHistoryID = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerFund", x => x.PlayerFundID);
                    table.ForeignKey(
                        name: "FK_PlayerFund_ClubPlayers_PlayerID",
                        column: x => x.PlayerID,
                        principalSchema: "tournament",
                        principalTable: "ClubPlayers",
                        principalColumn: "PlayerID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClubFunds",
                schema: "tournament",
                columns: table => new
                {
                    FundID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClubID = table.Column<int>(type: "int", nullable: false),
                    FundName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Expense = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Debt = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Contribution = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FundDescription = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FundAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FundDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FundType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FundStatus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubFundTeam", x => x.FundID);
                    table.ForeignKey(
                        name: "FK_ClubFundTeam_ClubTeam_ClubID",
                        column: x => x.ClubID,
                        principalSchema: "tournament",
                        principalTable: "ClubTeam",
                        principalColumn: "ClubID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlayerEvent",
                schema: "tournament",
                columns: table => new
                {
                    PlayerEventID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerID = table.Column<int>(type: "int", nullable: false),
                    ClubID = table.Column<int>(type: "int", nullable: false),
                    EventID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerEvent", x => x.PlayerEventID);
                    table.ForeignKey(
                        name: "FK_PlayerEvent_ClubPlayers_PlayerID",
                        column: x => x.PlayerID,
                        principalSchema: "tournament",
                        principalTable: "ClubPlayers",
                        principalColumn: "PlayerID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlayerEvent_ClubTeam_ClubID",
                        column: x => x.ClubID,
                        principalSchema: "tournament",
                        principalTable: "ClubTeam",
                        principalColumn: "ClubID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlayerLineUp",
                schema: "tournament",
                columns: table => new
                {
                    PlayerLineUpID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerID = table.Column<int>(type: "int", nullable: false),
                    ClubID = table.Column<int>(type: "int", nullable: false),
                    LineUpID = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsCaptain = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerLineUp", x => x.PlayerLineUpID);
                    table.ForeignKey(
                        name: "FK_PlayerLineUp_ClubPlayers_PlayerID",
                        column: x => x.PlayerID,
                        principalSchema: "tournament",
                        principalTable: "ClubPlayers",
                        principalColumn: "PlayerID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlayerLineUp_ClubTeam_ClubID",
                        column: x => x.ClubID,
                        principalSchema: "tournament",
                        principalTable: "ClubTeam",
                        principalColumn: "ClubID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlayerLineUp_LineUpBase_LineUpID",
                        column: x => x.LineUpID,
                        principalSchema: "tournament",
                        principalTable: "LineUpBase",
                        principalColumn: "LineUpID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                schema: "tournament",
                columns: table => new
                {
                    MatchesID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchesName = table.Column<int>(type: "int", nullable: false),
                    TeamA = table.Column<int>(type: "int", nullable: false),
                    TeamB = table.Column<int>(type: "int", nullable: false),
                    MatchesDescription = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TournamentID = table.Column<int>(type: "int", nullable: false),
                    Stadium = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.MatchesID);
                    table.ForeignKey(
                        name: "FK_Matches_TournamentBase_TournamentID",
                        column: x => x.TournamentID,
                        principalSchema: "tournament",
                        principalTable: "TournamentBase",
                        principalColumn: "TournamentID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TournamentClub",
                schema: "tournament",
                columns: table => new
                {
                    TournamentClubID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TournamentID = table.Column<int>(type: "int", nullable: false),
                    ClubID = table.Column<int>(type: "int", nullable: false),
                    Rank = table.Column<int>(type: "int", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false),
                    Played = table.Column<int>(type: "int", nullable: false),
                    Won = table.Column<int>(type: "int", nullable: false),
                    Drawn = table.Column<int>(type: "int", nullable: false),
                    Lost = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentClub", x => x.TournamentClubID);
                    table.ForeignKey(
                        name: "FK_TournamentClub_ClubTeam_ClubID",
                        column: x => x.ClubID,
                        principalSchema: "tournament",
                        principalTable: "ClubTeam",
                        principalColumn: "ClubID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TournamentClub_TournamentBase_TournamentID",
                        column: x => x.TournamentID,
                        principalSchema: "tournament",
                        principalTable: "TournamentBase",
                        principalColumn: "TournamentID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FundActionHistory",
                schema: "tournament",
                columns: table => new
                {
                    FundActionHistoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FundID = table.Column<int>(type: "int", nullable: false),
                    ClubID = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    playerMember = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FundActionType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PlayerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FundActionHistory", x => x.FundActionHistoryID);
                    table.ForeignKey(
                        name: "FK_FundActionHistory_ClubFundTeam_FundID",
                        column: x => x.FundID,
                        principalSchema: "tournament",
                        principalTable: "ClubFunds",
                        principalColumn: "FundID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FundActionHistory_ClubTeam_ClubID",
                        column: x => x.ClubID,
                        principalSchema: "tournament",
                        principalTable: "ClubTeam",
                        principalColumn: "ClubID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LineUpMatches",
                schema: "tournament",
                columns: table => new
                {
                    LineUpMatchesID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LineUpID = table.Column<int>(type: "int", nullable: false),
                    MatchID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineUpMatches", x => x.LineUpMatchesID);
                    table.ForeignKey(
                        name: "FK_LineUpMatches_LineUpBase_LineUpID",
                        column: x => x.LineUpID,
                        principalSchema: "tournament",
                        principalTable: "LineUpBase",
                        principalColumn: "LineUpID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LineUpMatches_Matches_MatchID",
                        column: x => x.MatchID,
                        principalSchema: "tournament",
                        principalTable: "Matches",
                        principalColumn: "MatchesID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MatchesStatistic",
                schema: "tournament",
                columns: table => new
                {
                    MatchesStatisticId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerID = table.Column<int>(type: "int", nullable: false),
                    LineUpID = table.Column<int>(type: "int", nullable: false),
                    ClubID = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    MatchesID = table.Column<int>(type: "int", nullable: false),
                    Shot = table.Column<int>(type: "int", nullable: false),
                    Pass = table.Column<int>(type: "int", nullable: false),
                    Fouls = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchesStatistic", x => x.MatchesStatisticId);
                    table.ForeignKey(
                        name: "FK_MatchesStatistic_ClubPlayers_PlayerID",
                        column: x => x.PlayerID,
                        principalSchema: "tournament",
                        principalTable: "ClubPlayers",
                        principalColumn: "PlayerID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MatchesStatistic_ClubTeam_ClubID",
                        column: x => x.ClubID,
                        principalSchema: "tournament",
                        principalTable: "ClubTeam",
                        principalColumn: "ClubID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MatchesStatistic_LineUpBase_LineUpID",
                        column: x => x.LineUpID,
                        principalSchema: "tournament",
                        principalTable: "LineUpBase",
                        principalColumn: "LineUpID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MatchesStatistic_Matches_MatchesID",
                        column: x => x.MatchesID,
                        principalSchema: "tournament",
                        principalTable: "Matches",
                        principalColumn: "MatchesID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Predictions",
                schema: "tournament",
                columns: table => new
                {
                    PredictionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MinigameID = table.Column<int>(type: "int", nullable: false),
                    PlayerID = table.Column<int>(type: "int", nullable: false),
                    MatchID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    Prediction = table.Column<int>(type: "int", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false),
                    PredictionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClubWin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScoreA = table.Column<int>(type: "int", nullable: false),
                    ScoreB = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Predictions", x => x.PredictionID);
                    table.ForeignKey(
                        name: "FK_Predictions_ClubPlayers_PlayerID",
                        column: x => x.PlayerID,
                        principalSchema: "tournament",
                        principalTable: "ClubPlayers",
                        principalColumn: "PlayerID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Predictions_Matches_MatchID",
                        column: x => x.MatchID,
                        principalSchema: "tournament",
                        principalTable: "Matches",
                        principalColumn: "MatchesID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Predictions_Minigames_MinigameID",
                        column: x => x.MinigameID,
                        principalSchema: "tournament",
                        principalTable: "Minigames",
                        principalColumn: "MinigameID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Votes",
                schema: "tournament",
                columns: table => new
                {
                    VoteID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MinigameID = table.Column<int>(type: "int", nullable: false),
                    PlayerID = table.Column<int>(type: "int", nullable: false),
                    MatchID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    Selection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false),
                    VoteDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votes", x => x.VoteID);
                    table.ForeignKey(
                        name: "FK_Votes_ClubPlayers_PlayerID",
                        column: x => x.PlayerID,
                        principalSchema: "tournament",
                        principalTable: "ClubPlayers",
                        principalColumn: "PlayerID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Votes_Matches_MatchID",
                        column: x => x.MatchID,
                        principalSchema: "tournament",
                        principalTable: "Matches",
                        principalColumn: "MatchesID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Votes_Minigames_MinigameID",
                        column: x => x.MinigameID,
                        principalSchema: "tournament",
                        principalTable: "Minigames",
                        principalColumn: "MinigameID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClubFundTeam_ClubID",
                schema: "tournament",
                table: "ClubFunds",
                column: "ClubID");

            migrationBuilder.CreateIndex(
                name: "IX_FundActionHistory_ClubID",
                schema: "tournament",
                table: "FundActionHistory",
                column: "ClubID");

            migrationBuilder.CreateIndex(
                name: "IX_FundActionHistory_FundID",
                schema: "tournament",
                table: "FundActionHistory",
                column: "FundID");

            migrationBuilder.CreateIndex(
                name: "IX_LineUpMatches_LineUpID",
                schema: "tournament",
                table: "LineUpMatches",
                column: "LineUpID");

            migrationBuilder.CreateIndex(
                name: "IX_LineUpMatches_MatchID",
                schema: "tournament",
                table: "LineUpMatches",
                column: "MatchID");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_TournamentID",
                schema: "tournament",
                table: "Matches",
                column: "TournamentID");

            migrationBuilder.CreateIndex(
                name: "IX_MatchesStatistic_ClubID",
                schema: "tournament",
                table: "MatchesStatistic",
                column: "ClubID");

            migrationBuilder.CreateIndex(
                name: "IX_MatchesStatistic_LineUpID",
                schema: "tournament",
                table: "MatchesStatistic",
                column: "LineUpID");

            migrationBuilder.CreateIndex(
                name: "IX_MatchesStatistic_MatchesID",
                schema: "tournament",
                table: "MatchesStatistic",
                column: "MatchesID");

            migrationBuilder.CreateIndex(
                name: "IX_MatchesStatistic_PlayerID",
                schema: "tournament",
                table: "MatchesStatistic",
                column: "PlayerID");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerEvent_ClubID",
                schema: "tournament",
                table: "PlayerEvent",
                column: "ClubID");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerEvent_PlayerID",
                schema: "tournament",
                table: "PlayerEvent",
                column: "PlayerID");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerFund_PlayerID",
                schema: "tournament",
                table: "PlayerFund",
                column: "PlayerID");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerLineUp_ClubID",
                schema: "tournament",
                table: "PlayerLineUp",
                column: "ClubID");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerLineUp_LineUpID",
                schema: "tournament",
                table: "PlayerLineUp",
                column: "LineUpID");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerLineUp_PlayerID",
                schema: "tournament",
                table: "PlayerLineUp",
                column: "PlayerID");

            migrationBuilder.CreateIndex(
                name: "IX_Predictions_MatchID",
                schema: "tournament",
                table: "Predictions",
                column: "MatchID");

            migrationBuilder.CreateIndex(
                name: "IX_Predictions_MinigameID",
                schema: "tournament",
                table: "Predictions",
                column: "MinigameID");

            migrationBuilder.CreateIndex(
                name: "IX_Predictions_PlayerID",
                schema: "tournament",
                table: "Predictions",
                column: "PlayerID");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentClub_ClubID",
                schema: "tournament",
                table: "TournamentClub",
                column: "ClubID");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentClub_TournamentID",
                schema: "tournament",
                table: "TournamentClub",
                column: "TournamentID");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_MatchID",
                schema: "tournament",
                table: "Votes",
                column: "MatchID");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_MinigameID",
                schema: "tournament",
                table: "Votes",
                column: "MinigameID");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_PlayerID",
                schema: "tournament",
                table: "Votes",
                column: "PlayerID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CelebrateEvent",
                schema: "tournament");

            migrationBuilder.DropTable(
                name: "FundActionHistory",
                schema: "tournament");

            migrationBuilder.DropTable(
                name: "LineUpMatches",
                schema: "tournament");

            migrationBuilder.DropTable(
                name: "MatchesStatistic",
                schema: "tournament");

            migrationBuilder.DropTable(
                name: "Orders",
                schema: "tournament");

            migrationBuilder.DropTable(
                name: "PlayerEvent",
                schema: "tournament");

            migrationBuilder.DropTable(
                name: "PlayerFund",
                schema: "tournament");

            migrationBuilder.DropTable(
                name: "PlayerLineUp",
                schema: "tournament");

            migrationBuilder.DropTable(
                name: "Predictions",
                schema: "tournament");

            migrationBuilder.DropTable(
                name: "TeamMeetingEvent",
                schema: "tournament");

            migrationBuilder.DropTable(
                name: "TournamentClub",
                schema: "tournament");

            migrationBuilder.DropTable(
                name: "TrainingEvent",
                schema: "tournament");

            migrationBuilder.DropTable(
                name: "Votes",
                schema: "tournament");

            migrationBuilder.DropTable(
                name: "ClubFunds",
                schema: "tournament");

            migrationBuilder.DropTable(
                name: "LineUpBase",
                schema: "tournament");

            migrationBuilder.DropTable(
                name: "ClubPlayers",
                schema: "tournament");

            migrationBuilder.DropTable(
                name: "Matches",
                schema: "tournament");

            migrationBuilder.DropTable(
                name: "Minigames",
                schema: "tournament");

            migrationBuilder.DropTable(
                name: "ClubTeam",
                schema: "tournament");

            migrationBuilder.DropTable(
                name: "TournamentBase",
                schema: "tournament");
        }
    }
}
