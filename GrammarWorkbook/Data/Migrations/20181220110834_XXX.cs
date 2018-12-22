using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GrammarWorkbook.Data.Migrations
{
    public partial class XXX : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sentence",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    FillTheBlanksExerciseId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sentence", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sentence_Exercise_FillTheBlanksExerciseId",
                        column: x => x.FillTheBlanksExerciseId,
                        principalTable: "Exercise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Dialog",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SentenceId = table.Column<Guid>(nullable: true),
                    DialogExerciseId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dialog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dialog_Exercise_DialogExerciseId",
                        column: x => x.DialogExerciseId,
                        principalTable: "Exercise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Dialog_Sentence_SentenceId",
                        column: x => x.SentenceId,
                        principalTable: "Sentence",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Option",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Value = table.Column<string>(nullable: true),
                    DialogId = table.Column<Guid>(nullable: true),
                    FillTheBlanksExerciseId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Option", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Option_Dialog_DialogId",
                        column: x => x.DialogId,
                        principalTable: "Dialog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Option_Exercise_FillTheBlanksExerciseId",
                        column: x => x.FillTheBlanksExerciseId,
                        principalTable: "Exercise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dialog_DialogExerciseId",
                table: "Dialog",
                column: "DialogExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_Dialog_SentenceId",
                table: "Dialog",
                column: "SentenceId");

            migrationBuilder.CreateIndex(
                name: "IX_Option_DialogId",
                table: "Option",
                column: "DialogId");

            migrationBuilder.CreateIndex(
                name: "IX_Option_FillTheBlanksExerciseId",
                table: "Option",
                column: "FillTheBlanksExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_Sentence_FillTheBlanksExerciseId",
                table: "Sentence",
                column: "FillTheBlanksExerciseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Option");

            migrationBuilder.DropTable(
                name: "Dialog");

            migrationBuilder.DropTable(
                name: "Sentence");
        }
    }
}
