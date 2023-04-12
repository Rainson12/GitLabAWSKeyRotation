using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GitLabAWSKeyRotation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AWSAccounts",
                columns: table => new
                {
                    AccountId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    AwsAccountId = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DisplayName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AWSAccounts", x => x.AccountId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CodeRepositories",
                columns: table => new
                {
                    CodeRepositoryId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Url = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AccessKey_Identifier = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AccessKey_Token = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodeRepositories", x => x.CodeRepositoryId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AWSIAMs",
                columns: table => new
                {
                    IamId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Arn = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AccessKeyId = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AccessSecret = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    KeyRotationInDays = table.Column<float>(type: "float", nullable: false),
                    AWSAccountId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AWSIAMs", x => x.IamId);
                    table.ForeignKey(
                        name: "FK_AWSIAMs_AWSAccounts_AWSAccountId",
                        column: x => x.AWSAccountId,
                        principalTable: "AWSAccounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RotationDependentRepositoryVariables",
                columns: table => new
                {
                    RotationDependentRepositoryVariablesId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Environment = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AccessKeyIdVariableName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AccessSecretVariableName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CodeRepositoryId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    IamId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RotationDependentRepositoryVariables", x => x.RotationDependentRepositoryVariablesId);
                    table.ForeignKey(
                        name: "FK_RotationDependentRepositoryVariables_AWSIAMs_IamId",
                        column: x => x.IamId,
                        principalTable: "AWSIAMs",
                        principalColumn: "IamId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RotationDependentRepositoryVariables_CodeRepositories_CodeRe~",
                        column: x => x.CodeRepositoryId,
                        principalTable: "CodeRepositories",
                        principalColumn: "CodeRepositoryId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_AWSIAMs_AWSAccountId",
                table: "AWSIAMs",
                column: "AWSAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_RotationDependentRepositoryVariables_CodeRepositoryId",
                table: "RotationDependentRepositoryVariables",
                column: "CodeRepositoryId");

            migrationBuilder.CreateIndex(
                name: "IX_RotationDependentRepositoryVariables_IamId",
                table: "RotationDependentRepositoryVariables",
                column: "IamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RotationDependentRepositoryVariables");

            migrationBuilder.DropTable(
                name: "AWSIAMs");

            migrationBuilder.DropTable(
                name: "CodeRepositories");

            migrationBuilder.DropTable(
                name: "AWSAccounts");
        }
    }
}
