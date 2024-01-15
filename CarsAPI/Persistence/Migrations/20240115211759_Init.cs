using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Path = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    ShortDescription = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ContentHtml = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PropCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Priority = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Models",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CarTypeId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Models", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Models_CarTypes_CarTypeId",
                        column: x => x.CarTypeId,
                        principalTable: "CarTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsKeyProperty = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    PropCategoryId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Properties_PropCategories_PropCategoryId",
                        column: x => x.PropCategoryId,
                        principalTable: "PropCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Price = table.Column<int>(type: "integer", nullable: false),
                    ProductionYear = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ModelId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_Models_ModelId",
                        column: x => x.ModelId,
                        principalTable: "Models",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PropertyId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropValues_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Car_Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IsMainImage = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    ImageId = table.Column<Guid>(type: "uuid", nullable: false),
                    CarId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Car_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Car_Images_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Car_Images_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Car_PropValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CarId = table.Column<Guid>(type: "uuid", nullable: false),
                    PropValueId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Car_PropValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Car_PropValues_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Car_PropValues_PropValues_PropValueId",
                        column: x => x.PropValueId,
                        principalTable: "PropValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Car_Images_CarId",
                table: "Car_Images",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Car_Images_Id",
                table: "Car_Images",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Car_Images_ImageId",
                table: "Car_Images",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Car_PropValues_CarId",
                table: "Car_PropValues",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Car_PropValues_Id",
                table: "Car_PropValues",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Car_PropValues_PropValueId",
                table: "Car_PropValues",
                column: "PropValueId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_Id",
                table: "Cars",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cars_ModelId",
                table: "Cars",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_CarTypes_Id",
                table: "CarTypes",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_Id",
                table: "Images",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Models_CarTypeId",
                table: "Models",
                column: "CarTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Models_Id",
                table: "Models",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_News_Id",
                table: "News",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PropCategories_Id",
                table: "PropCategories",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Properties_Id",
                table: "Properties",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Properties_PropCategoryId",
                table: "Properties",
                column: "PropCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PropValues_Id",
                table: "PropValues",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PropValues_PropertyId",
                table: "PropValues",
                column: "PropertyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Car_Images");

            migrationBuilder.DropTable(
                name: "Car_PropValues");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "PropValues");

            migrationBuilder.DropTable(
                name: "Models");

            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.DropTable(
                name: "CarTypes");

            migrationBuilder.DropTable(
                name: "PropCategories");
        }
    }
}
