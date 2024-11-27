using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SM.WebAPI.Migrations.TournamentDb
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
                name: "ClubEventBase",
                columns: table => new
                {
                    EventID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EventDescription = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EventDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EventLocation = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EventStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    membersCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubEventBase", x => x.EventID);
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
                name: "Invoices",
                schema: "tournament",
                columns: table => new
                {
                    InvoiceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.InvoiceID);
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
                });

            migrationBuilder.CreateTable(
                name: "MinigameReward",
                schema: "tournament",
                columns: table => new
                {
                    MinigameRewardID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RewardName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RewardDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RewardType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RewardValue = table.Column<int>(type: "int", nullable: false),
                    createDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Rank = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MinigameReward", x => x.MinigameRewardID);
                });

            migrationBuilder.CreateTable(
                name: "TournamentBase",
                schema: "tournament",
                columns: table => new
                {
                    TournamentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    TournamentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TournamentPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TournamentDescription = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TournamentType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TournamentStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TournamentLocation = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TournamentOrganizer = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TournamentContact = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    numberMember = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentBase", x => x.TournamentID);
                });

            migrationBuilder.CreateTable(
                name: "TeamMeetingEvent",
                schema: "tournament",
                columns: table => new
                {
                    EventID = table.Column<int>(type: "int", nullable: false),
                    ClubID = table.Column<int>(type: "int", nullable: false),
                    MeetingAim = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MeetingContent = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamMeetingEvent", x => x.EventID);
                    table.ForeignKey(
                        name: "FK_TeamMeetingEvent_ClubEventBase_EventID",
                        column: x => x.EventID,
                        principalTable: "ClubEventBase",
                        principalColumn: "EventID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainingEvent",
                schema: "tournament",
                columns: table => new
                {
                    EventID = table.Column<int>(type: "int", nullable: false),
                    ClubID = table.Column<int>(type: "int", nullable: false),
                    TrainingAim = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LessonTraining = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Coach = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProcessResult = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingEvent", x => x.EventID);
                    table.ForeignKey(
                        name: "FK_TrainingEvent_ClubEventBase_EventID",
                        column: x => x.EventID,
                        principalTable: "ClubEventBase",
                        principalColumn: "EventID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CelebrateEvent",
                schema: "tournament",
                columns: table => new
                {
                    EventID = table.Column<int>(type: "int", nullable: false),
                    ClubID = table.Column<int>(type: "int", nullable: false),
                    Decor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Menu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Music = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    minigame = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CelebrateEvent", x => x.EventID);
                    table.ForeignKey(
                        name: "FK_CelebrateEvent_ClubEventBase_EventID",
                        column: x => x.EventID,
                        principalTable: "ClubEventBase",
                        principalColumn: "EventID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CelebrateEvent_ClubTeam_ClubID",
                        column: x => x.ClubID,
                        principalSchema: "tournament",
                        principalTable: "ClubTeam",
                        principalColumn: "ClubID",
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
                    Expense = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Debt = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Contribution = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    FundDescription = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FundAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    FundDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FundType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FundStatus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubFunds", x => x.FundID);
                    table.ForeignKey(
                        name: "FK_ClubFunds_ClubTeam_ClubID",
                        column: x => x.ClubID,
                        principalSchema: "tournament",
                        principalTable: "ClubTeam",
                        principalColumn: "ClubID",
                        onDelete: ReferentialAction.Restrict);
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
                    table.ForeignKey(
                        name: "FK_ClubPlayers_ClubTeam_ClubID",
                        column: x => x.ClubID,
                        principalSchema: "tournament",
                        principalTable: "ClubTeam",
                        principalColumn: "ClubID",
                        onDelete: ReferentialAction.Restrict);
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
                    table.ForeignKey(
                        name: "FK_LineUpBase_ClubTeam_ClubID",
                        column: x => x.ClubID,
                        principalSchema: "tournament",
                        principalTable: "ClubTeam",
                        principalColumn: "ClubID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                schema: "tournament",
                columns: table => new
                {
                    MatchesID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchesName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeamA = table.Column<int>(type: "int", nullable: false),
                    TeamB = table.Column<int>(type: "int", nullable: false),
                    MatchesDescription = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TournamentID = table.Column<int>(type: "int", nullable: false),
                    Stadium = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsFinish = table.Column<bool>(type: "bit", nullable: false),
                    Round = table.Column<int>(type: "int", nullable: false),
                    HomeScore = table.Column<int>(type: "int", nullable: true),
                    AwayScore = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.MatchesID);
                    table.ForeignKey(
                        name: "FK_Matches_ClubTeam_TeamA",
                        column: x => x.TeamA,
                        principalSchema: "tournament",
                        principalTable: "ClubTeam",
                        principalColumn: "ClubID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_ClubTeam_TeamB",
                        column: x => x.TeamB,
                        principalSchema: "tournament",
                        principalTable: "ClubTeam",
                        principalColumn: "ClubID",
                        onDelete: ReferentialAction.Restrict);
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
                    PaymentMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TournamentID = table.Column<int>(type: "int", nullable: false),
                    PaymentID = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_Orders_TournamentBase_TournamentID",
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
                    Lost = table.Column<int>(type: "int", nullable: false),
                    GoalFor = table.Column<int>(type: "int", nullable: false),
                    GoatGet = table.Column<int>(type: "int", nullable: false)
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
                        name: "FK_FundActionHistory_ClubFunds_FundID",
                        column: x => x.FundID,
                        principalSchema: "tournament",
                        principalTable: "ClubFunds",
                        principalColumn: "FundID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FundActionHistory_ClubPlayers_PlayerID",
                        column: x => x.PlayerID,
                        principalSchema: "tournament",
                        principalTable: "ClubPlayers",
                        principalColumn: "PlayerID",
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
                    EventID = table.Column<int>(type: "int", nullable: false),
                    EventType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JoinDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerEvent", x => x.PlayerEventID);
                    table.ForeignKey(
                        name: "FK_PlayerEvent_ClubEventBase_EventID",
                        column: x => x.EventID,
                        principalTable: "ClubEventBase",
                        principalColumn: "EventID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlayerEvent_ClubPlayers_PlayerID",
                        column: x => x.PlayerID,
                        principalSchema: "tournament",
                        principalTable: "ClubPlayers",
                        principalColumn: "PlayerID",
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
                        name: "FK_PlayerLineUp_LineUpBase_LineUpID",
                        column: x => x.LineUpID,
                        principalSchema: "tournament",
                        principalTable: "LineUpBase",
                        principalColumn: "LineUpID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MatchesStatistic",
                schema: "tournament",
                columns: table => new
                {
                    MatchesStatisticID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerID = table.Column<int>(type: "int", nullable: false),
                    LineUpID = table.Column<int>(type: "int", nullable: false),
                    ClubID = table.Column<int>(type: "int", nullable: false),
                    half = table.Column<int>(type: "int", nullable: false),
                    MatchesID = table.Column<int>(type: "int", nullable: false),
                    Shot = table.Column<int>(type: "int", nullable: false),
                    Pass = table.Column<int>(type: "int", nullable: false),
                    Fouls = table.Column<int>(type: "int", nullable: false),
                    RedCard = table.Column<int>(type: "int", nullable: false),
                    YellowCard = table.Column<int>(type: "int", nullable: false),
                    Assist = table.Column<int>(type: "int", nullable: false),
                    Goal = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchesStatistic", x => x.MatchesStatisticID);
                    table.ForeignKey(
                        name: "FK_MatchesStatistic_ClubPlayers_PlayerID",
                        column: x => x.PlayerID,
                        principalSchema: "tournament",
                        principalTable: "ClubPlayers",
                        principalColumn: "PlayerID",
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
                name: "Minigames",
                schema: "tournament",
                columns: table => new
                {
                    MinigameID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TournamentID = table.Column<int>(type: "int", nullable: false),
                    MatchesID = table.Column<int>(type: "int", nullable: false),
                    MinigameType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDates = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDates = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MinigameRewardID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Handicap = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Minigames", x => x.MinigameID);
                    table.ForeignKey(
                        name: "FK_Minigames_Matches_MatchesID",
                        column: x => x.MatchesID,
                        principalSchema: "tournament",
                        principalTable: "Matches",
                        principalColumn: "MatchesID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Minigames_MinigameReward_MinigameRewardID",
                        column: x => x.MinigameRewardID,
                        principalSchema: "tournament",
                        principalTable: "MinigameReward",
                        principalColumn: "MinigameRewardID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Minigames_TournamentBase_TournamentID",
                        column: x => x.TournamentID,
                        principalSchema: "tournament",
                        principalTable: "TournamentBase",
                        principalColumn: "TournamentID",
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
                    UserID = table.Column<int>(type: "int", nullable: false),
                    TeamAscore = table.Column<int>(type: "int", nullable: true),
                    TeamBscore = table.Column<int>(type: "int", nullable: true),
                    PredictTotal = table.Column<int>(type: "int", nullable: true),
                    OddEven = table.Column<bool>(type: "bit", nullable: true),
                    half = table.Column<int>(type: "int", nullable: true),
                    PredictionDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Predictions", x => x.PredictionID);
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
                    MatchID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    Selection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VoteDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votes", x => x.VoteID);
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

            migrationBuilder.CreateTable(
                name: "PredictionRanking",
                schema: "tournament",
                columns: table => new
                {
                    PredictionRankingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    PredictionTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MinigameID = table.Column<int>(type: "int", nullable: false),
                    Rank = table.Column<int>(type: "int", nullable: false),
                    PredictionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PredictionID = table.Column<int>(type: "int", nullable: false),
                    MinigameRewardID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PredictionRanking", x => x.PredictionRankingID);
                    table.ForeignKey(
                        name: "FK_PredictionRanking_MinigameReward_MinigameRewardID",
                        column: x => x.MinigameRewardID,
                        principalSchema: "tournament",
                        principalTable: "MinigameReward",
                        principalColumn: "MinigameRewardID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PredictionRanking_Minigames_MinigameID",
                        column: x => x.MinigameID,
                        principalSchema: "tournament",
                        principalTable: "Minigames",
                        principalColumn: "MinigameID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PredictionRanking_Predictions_PredictionID",
                        column: x => x.PredictionID,
                        principalSchema: "tournament",
                        principalTable: "Predictions",
                        principalColumn: "PredictionID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CelebrateEvent_ClubID",
                schema: "tournament",
                table: "CelebrateEvent",
                column: "ClubID");

            migrationBuilder.CreateIndex(
                name: "IX_ClubFunds_ClubID",
                schema: "tournament",
                table: "ClubFunds",
                column: "ClubID");

            migrationBuilder.CreateIndex(
                name: "IX_ClubPlayers_ClubID",
                schema: "tournament",
                table: "ClubPlayers",
                column: "ClubID");

            migrationBuilder.CreateIndex(
                name: "IX_FundActionHistory_FundID",
                schema: "tournament",
                table: "FundActionHistory",
                column: "FundID");

            migrationBuilder.CreateIndex(
                name: "IX_FundActionHistory_PlayerID",
                schema: "tournament",
                table: "FundActionHistory",
                column: "PlayerID");

            migrationBuilder.CreateIndex(
                name: "IX_LineUpBase_ClubID",
                schema: "tournament",
                table: "LineUpBase",
                column: "ClubID");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_TeamA",
                schema: "tournament",
                table: "Matches",
                column: "TeamA");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_TeamB",
                schema: "tournament",
                table: "Matches",
                column: "TeamB");

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
                name: "IX_Minigames_MatchesID",
                schema: "tournament",
                table: "Minigames",
                column: "MatchesID");

            migrationBuilder.CreateIndex(
                name: "IX_Minigames_MinigameRewardID",
                schema: "tournament",
                table: "Minigames",
                column: "MinigameRewardID");

            migrationBuilder.CreateIndex(
                name: "IX_Minigames_TournamentID",
                schema: "tournament",
                table: "Minigames",
                column: "TournamentID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_TournamentID",
                schema: "tournament",
                table: "Orders",
                column: "TournamentID");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerEvent_EventID",
                schema: "tournament",
                table: "PlayerEvent",
                column: "EventID");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerEvent_PlayerID",
                schema: "tournament",
                table: "PlayerEvent",
                column: "PlayerID");

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
                name: "IX_PredictionRanking_MinigameID",
                schema: "tournament",
                table: "PredictionRanking",
                column: "MinigameID");

            migrationBuilder.CreateIndex(
                name: "IX_PredictionRanking_MinigameRewardID",
                schema: "tournament",
                table: "PredictionRanking",
                column: "MinigameRewardID");

            migrationBuilder.CreateIndex(
                name: "IX_PredictionRanking_PredictionID",
                schema: "tournament",
                table: "PredictionRanking",
                column: "PredictionID");

            migrationBuilder.CreateIndex(
                name: "IX_Predictions_MinigameID",
                schema: "tournament",
                table: "Predictions",
                column: "MinigameID");

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
                name: "Invoices",
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
                name: "PlayerLineUp",
                schema: "tournament");

            migrationBuilder.DropTable(
                name: "PredictionRanking",
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
                name: "ClubPlayers",
                schema: "tournament");

            migrationBuilder.DropTable(
                name: "LineUpBase",
                schema: "tournament");

            migrationBuilder.DropTable(
                name: "Predictions",
                schema: "tournament");

            migrationBuilder.DropTable(
                name: "ClubEventBase");

            migrationBuilder.DropTable(
                name: "Minigames",
                schema: "tournament");

            migrationBuilder.DropTable(
                name: "Matches",
                schema: "tournament");

            migrationBuilder.DropTable(
                name: "MinigameReward",
                schema: "tournament");

            migrationBuilder.DropTable(
                name: "TournamentBase",
                schema: "tournament");

            migrationBuilder.DropTable(
                name: "ClubTeam",
                schema: "tournament");
        }
    }
}
