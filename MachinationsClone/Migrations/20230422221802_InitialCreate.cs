using System;
using System.Collections.Generic;
using MachinationsClone.Models.Entities.Graph;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MachinationsClone.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConnectionTypes",
                columns: table => new
                {
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    LineType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConnectionTypes", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "NodeTypes",
                columns: table => new
                {
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Symbol = table.Column<string>(type: "text", nullable: true),
                    Exportable = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NodeTypes", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GraphElements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GraphId = table.Column<Guid>(type: "uuid", nullable: false),
                    Properties = table.Column<Dictionary<string, string>>(type: "jsonb", nullable: true),
                    Discriminator = table.Column<string>(type: "text", nullable: false),
                    ConnectionTypeName = table.Column<string>(type: "text", nullable: true),
                    StartId = table.Column<Guid>(type: "uuid", nullable: true),
                    EndId = table.Column<Guid>(type: "uuid", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    ActivationMode = table.Column<int>(type: "integer", nullable: true),
                    PullMode = table.Column<int>(type: "integer", nullable: true),
                    NodeTypeName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GraphElements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GraphElements_ConnectionTypes_ConnectionTypeName",
                        column: x => x.ConnectionTypeName,
                        principalTable: "ConnectionTypes",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GraphElements_GraphElements_EndId",
                        column: x => x.EndId,
                        principalTable: "GraphElements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GraphElements_GraphElements_StartId",
                        column: x => x.StartId,
                        principalTable: "GraphElements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GraphElements_NodeTypes_NodeTypeName",
                        column: x => x.NodeTypeName,
                        principalTable: "NodeTypes",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NodeGeometries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GraphNodeId = table.Column<Guid>(type: "uuid", nullable: false),
                    X = table.Column<double>(type: "double precision", nullable: false),
                    Y = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NodeGeometries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NodeGeometries_GraphElements_GraphNodeId",
                        column: x => x.GraphNodeId,
                        principalTable: "GraphElements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GraphStates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GraphId = table.Column<Guid>(type: "uuid", nullable: false),
                    GraphStatesGroupId = table.Column<Guid>(type: "uuid", nullable: true),
                    Step = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    GraphElementStates = table.Column<Dictionary<Guid, GraphElementState>>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GraphStates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GraphStatesGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    GraphId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TotalSteps = table.Column<int>(type: "integer", nullable: false),
                    LastStateId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GraphStatesGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GraphStatesGroups_GraphStates_LastStateId",
                        column: x => x.LastStateId,
                        principalTable: "GraphStates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Graphs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    StepSize = table.Column<int>(type: "integer", nullable: false),
                    CurrentStatesGroupId = table.Column<Guid>(type: "uuid", nullable: true),
                    CurrentStateId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Graphs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Graphs_GraphStates_CurrentStateId",
                        column: x => x.CurrentStateId,
                        principalTable: "GraphStates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Graphs_GraphStatesGroups_CurrentStatesGroupId",
                        column: x => x.CurrentStatesGroupId,
                        principalTable: "GraphStatesGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Graphs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ConnectionTypes",
                columns: new[] { "Name", "Description", "LineType" },
                values: new object[] { "resourceConnection", "A resource connection is an edge with an associated expression that defines the rate at which resources can flow between source and target nodes.", 0 });

            migrationBuilder.InsertData(
                table: "NodeTypes",
                columns: new[] { "Name", "Description", "Exportable", "Symbol" },
                values: new object[] { "pool", "A pool is a named node, that abstracts from an in-game entity, and can contain resources, such as coins, crystals, health, etc.", true, "pool" });

            migrationBuilder.CreateIndex(
                name: "IX_GraphElements_ConnectionTypeName",
                table: "GraphElements",
                column: "ConnectionTypeName");

            migrationBuilder.CreateIndex(
                name: "IX_GraphElements_EndId",
                table: "GraphElements",
                column: "EndId");

            migrationBuilder.CreateIndex(
                name: "IX_GraphElements_GraphId",
                table: "GraphElements",
                column: "GraphId");

            migrationBuilder.CreateIndex(
                name: "IX_GraphElements_NodeTypeName",
                table: "GraphElements",
                column: "NodeTypeName");

            migrationBuilder.CreateIndex(
                name: "IX_GraphElements_StartId",
                table: "GraphElements",
                column: "StartId");

            migrationBuilder.CreateIndex(
                name: "IX_Graphs_CurrentStateId",
                table: "Graphs",
                column: "CurrentStateId");

            migrationBuilder.CreateIndex(
                name: "IX_Graphs_CurrentStatesGroupId",
                table: "Graphs",
                column: "CurrentStatesGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Graphs_UserId",
                table: "Graphs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GraphStates_GraphId",
                table: "GraphStates",
                column: "GraphId");

            migrationBuilder.CreateIndex(
                name: "IX_GraphStates_GraphStatesGroupId",
                table: "GraphStates",
                column: "GraphStatesGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GraphStatesGroups_GraphId",
                table: "GraphStatesGroups",
                column: "GraphId");

            migrationBuilder.CreateIndex(
                name: "IX_GraphStatesGroups_LastStateId",
                table: "GraphStatesGroups",
                column: "LastStateId");

            migrationBuilder.CreateIndex(
                name: "IX_NodeGeometries_GraphNodeId",
                table: "NodeGeometries",
                column: "GraphNodeId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GraphElements_Graphs_GraphId",
                table: "GraphElements",
                column: "GraphId",
                principalTable: "Graphs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GraphStates_Graphs_GraphId",
                table: "GraphStates",
                column: "GraphId",
                principalTable: "Graphs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GraphStates_GraphStatesGroups_GraphStatesGroupId",
                table: "GraphStates",
                column: "GraphStatesGroupId",
                principalTable: "GraphStatesGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GraphStatesGroups_Graphs_GraphId",
                table: "GraphStatesGroups",
                column: "GraphId",
                principalTable: "Graphs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GraphStates_Graphs_GraphId",
                table: "GraphStates");

            migrationBuilder.DropForeignKey(
                name: "FK_GraphStatesGroups_Graphs_GraphId",
                table: "GraphStatesGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_GraphStatesGroups_GraphStates_LastStateId",
                table: "GraphStatesGroups");

            migrationBuilder.DropTable(
                name: "NodeGeometries");

            migrationBuilder.DropTable(
                name: "GraphElements");

            migrationBuilder.DropTable(
                name: "ConnectionTypes");

            migrationBuilder.DropTable(
                name: "NodeTypes");

            migrationBuilder.DropTable(
                name: "Graphs");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "GraphStates");

            migrationBuilder.DropTable(
                name: "GraphStatesGroups");
        }
    }
}
