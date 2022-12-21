using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JCPBackend.Migrations.Postgres
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "j_customers",
                columns: table => new
                {
                    id = table.Column<string>(type: "character(36)", unicode: false, fixedLength: true, maxLength: 36, nullable: false),
                    title = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    surname = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    mobileno = table.Column<string>(name: "mobile_no", type: "character varying(12)", unicode: false, maxLength: 12, nullable: false),
                    homeno = table.Column<string>(name: "home_no", type: "character varying(12)", unicode: false, maxLength: 12, nullable: true),
                    workno = table.Column<string>(name: "work_no", type: "character varying(50)", maxLength: 50, nullable: true),
                    altno = table.Column<string>(name: "alt_no", type: "character varying(50)", maxLength: 50, nullable: true),
                    email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    type = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    regnumber = table.Column<string>(name: "reg_number", type: "character varying(50)", maxLength: 50, nullable: true),
                    vatno = table.Column<string>(name: "vat_no", type: "character varying(50)", maxLength: 50, nullable: true),
                    companyname = table.Column<string>(name: "company_name", type: "character varying(100)", maxLength: 100, nullable: true),
                    addressline1 = table.Column<string>(name: "address_line_1", type: "character varying(50)", maxLength: 50, nullable: true),
                    addressline2 = table.Column<string>(name: "address_line_2", type: "character varying(50)", maxLength: 50, nullable: true),
                    addressline3 = table.Column<string>(name: "address_line_3", type: "character varying(50)", maxLength: 50, nullable: true),
                    postal = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    siteaccessid = table.Column<string>(name: "site_access_id", type: "character(36)", unicode: false, fixedLength: true, maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_j_customers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "j_job_codes",
                columns: table => new
                {
                    code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    siteaccessid = table.Column<string>(name: "site_access_id", type: "character(36)", unicode: false, fixedLength: true, maxLength: 36, nullable: false),
                    description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    location = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    cost = table.Column<int>(type: "integer", nullable: false),
                    markup = table.Column<int>(type: "integer", nullable: false),
                    standardhours = table.Column<int>(name: "standard_hours", type: "integer", nullable: false),
                    standardvolume = table.Column<int>(name: "standard_volume", type: "integer", nullable: false),
                    labourrate = table.Column<int>(name: "labour_rate", type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_j_job_codes", x => new { x.code, x.siteaccessid });
                });

            migrationBuilder.CreateTable(
                name: "j_quote_status",
                columns: table => new
                {
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    sortorder = table.Column<int>(name: "sort_order", type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_j_quote_status", x => x.status);
                });

            migrationBuilder.CreateTable(
                name: "j_sites",
                columns: table => new
                {
                    id = table.Column<string>(type: "character(36)", unicode: false, fixedLength: true, maxLength: 36, nullable: false),
                    description = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_j_sites", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "j_suppliers",
                columns: table => new
                {
                    id = table.Column<string>(type: "character(36)", unicode: false, fixedLength: true, maxLength: 36, nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    regno = table.Column<string>(name: "reg_no", type: "character varying(50)", maxLength: 50, nullable: true),
                    vatno = table.Column<string>(name: "vat_no", type: "character varying(50)", maxLength: 50, nullable: true),
                    taxclearance = table.Column<string>(name: "tax_clearance", type: "character varying(50)", maxLength: 50, nullable: true),
                    creditlimit = table.Column<int>(name: "credit_limit", type: "integer", nullable: true),
                    creditbalance = table.Column<int>(name: "credit_balance", type: "integer", nullable: true),
                    telnum = table.Column<string>(name: "tel_num", type: "character varying(12)", maxLength: 12, nullable: true),
                    addressline1 = table.Column<string>(name: "address_line_1", type: "character varying(50)", maxLength: 50, nullable: true),
                    addressline2 = table.Column<string>(name: "address_line_2", type: "character varying(50)", maxLength: 50, nullable: true),
                    addressline3 = table.Column<string>(name: "address_line_3", type: "character varying(50)", maxLength: 50, nullable: true),
                    postal = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    contactperson = table.Column<string>(name: "contact_person", type: "character varying(50)", maxLength: 50, nullable: true),
                    contactno = table.Column<string>(name: "contact_no", type: "character varying(12)", maxLength: 12, nullable: true),
                    email = table.Column<string>(type: "character varying(320)", maxLength: 320, nullable: true),
                    afterhoursno = table.Column<string>(name: "after_hours_no", type: "character varying(12)", maxLength: 12, nullable: true),
                    standbyperson = table.Column<string>(name: "standby_person", type: "character varying(50)", maxLength: 50, nullable: true),
                    standbyno = table.Column<string>(name: "standby_no", type: "character varying(12)", maxLength: 12, nullable: true),
                    standbyemail = table.Column<string>(name: "standby_email", type: "character varying(320)", maxLength: 320, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_j_suppliers_1", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "j_techs",
                columns: table => new
                {
                    id = table.Column<string>(type: "character(36)", unicode: false, fixedLength: true, maxLength: 36, nullable: false),
                    name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    surname = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    siteaccessid = table.Column<string>(name: "site_access_id", type: "character(36)", unicode: false, fixedLength: true, maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_j_techs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "j_users",
                columns: table => new
                {
                    id = table.Column<string>(type: "character(36)", unicode: false, fixedLength: true, maxLength: 36, nullable: false),
                    username = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    surname = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    telno = table.Column<string>(name: "tel_no", type: "character varying(50)", unicode: false, maxLength: 50, nullable: true),
                    active = table.Column<bool>(type: "boolean", nullable: false),
                    passworddate = table.Column<DateTime>(name: "password_date", type: "date", nullable: false),
                    enddate = table.Column<DateTime>(name: "end_date", type: "date", nullable: false),
                    role = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_j_users_1", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "j_vehicle_models",
                columns: table => new
                {
                    brand = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    model = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_j_vehicle_models", x => new { x.brand, x.model });
                });

            migrationBuilder.CreateTable(
                name: "j_vehicles",
                columns: table => new
                {
                    id = table.Column<string>(type: "character(36)", unicode: false, fixedLength: true, maxLength: 36, nullable: false),
                    customerid = table.Column<string>(name: "customer_id", type: "character(36)", unicode: false, fixedLength: true, maxLength: 36, nullable: false),
                    vinnumber = table.Column<string>(name: "vin_number", type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    enginenumber = table.Column<string>(name: "engine_number", type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    registration = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    brand = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    model = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    year = table.Column<int>(type: "integer", nullable: false),
                    siteaccessid = table.Column<string>(name: "site_access_id", type: "character(36)", unicode: false, fixedLength: true, maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_j_vehicles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "j_supplier_branches",
                columns: table => new
                {
                    id = table.Column<string>(type: "character(36)", unicode: false, fixedLength: true, maxLength: 36, nullable: false),
                    supplierid = table.Column<string>(name: "supplier_id", type: "character(36)", unicode: false, fixedLength: true, maxLength: 36, nullable: true),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    addressline1 = table.Column<string>(name: "address_line_1", type: "character varying(50)", maxLength: 50, nullable: true),
                    addressline2 = table.Column<string>(name: "address_line_2", type: "character varying(50)", maxLength: 50, nullable: true),
                    addressline3 = table.Column<string>(name: "address_line_3", type: "character varying(50)", maxLength: 50, nullable: true),
                    postal = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    lat = table.Column<decimal>(type: "numeric(16,8)", nullable: true),
                    lgn = table.Column<decimal>(type: "numeric(16,8)", nullable: true),
                    contactperson = table.Column<string>(name: "contact_person", type: "character varying(50)", maxLength: 50, nullable: true),
                    contactnumber = table.Column<string>(name: "contact_number", type: "character varying(50)", maxLength: 50, nullable: true),
                    email = table.Column<string>(type: "character varying(320)", maxLength: 320, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_j_suppliers", x => x.id);
                    table.ForeignKey(
                        name: "FK_j_supplier_branches_j_suppliers",
                        column: x => x.supplierid,
                        principalTable: "j_suppliers",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "j_user_site_access",
                columns: table => new
                {
                    userid = table.Column<string>(name: "user_id", type: "character(36)", nullable: false),
                    siteid = table.Column<string>(name: "site_id", type: "character(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_j_user_site_access", x => new { x.userid, x.siteid });
                    table.ForeignKey(
                        name: "FK_j_user_site_access_j_sites",
                        column: x => x.siteid,
                        principalTable: "j_sites",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_j_user_site_access_j_user_site_access",
                        column: x => x.userid,
                        principalTable: "j_users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "j_quotes",
                columns: table => new
                {
                    id = table.Column<string>(type: "character(36)", unicode: false, fixedLength: true, maxLength: 36, nullable: false),
                    ronumber = table.Column<string>(name: "ro_number", type: "character varying(20)", maxLength: 20, nullable: false),
                    branchid = table.Column<string>(name: "branch_id", type: "character(36)", unicode: false, fixedLength: true, maxLength: 36, nullable: false),
                    customerid = table.Column<string>(name: "customer_id", type: "character(36)", unicode: false, fixedLength: true, maxLength: 36, nullable: false),
                    vehicleid = table.Column<string>(name: "vehicle_id", type: "character(36)", unicode: false, fixedLength: true, maxLength: 36, nullable: false),
                    createuserid = table.Column<string>(name: "create_user_id", type: "character(36)", unicode: false, fixedLength: true, maxLength: 36, nullable: false),
                    createdatetime = table.Column<DateTime>(name: "create_datetime", type: "timestamp with time zone", nullable: false),
                    updateuserid = table.Column<string>(name: "update_user_id", type: "character(36)", unicode: false, fixedLength: true, maxLength: 36, nullable: false),
                    updatedatetime = table.Column<DateTime>(name: "update_datetime", type: "timestamp with time zone", nullable: false),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    checkinodometer = table.Column<int>(name: "checkin_odometer", type: "integer", nullable: false),
                    techid = table.Column<string>(name: "tech_id", type: "character(36)", unicode: false, fixedLength: true, maxLength: 36, nullable: true),
                    siteaccess = table.Column<string>(name: "site_access", type: "character(36)", unicode: false, fixedLength: true, maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_j_quotes", x => x.id);
                    table.ForeignKey(
                        name: "FK_create_user",
                        column: x => x.createuserid,
                        principalTable: "j_users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_customer",
                        column: x => x.customerid,
                        principalTable: "j_customers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_tech",
                        column: x => x.techid,
                        principalTable: "j_techs",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_update_user",
                        column: x => x.updateuserid,
                        principalTable: "j_users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_vehicle",
                        column: x => x.vehicleid,
                        principalTable: "j_vehicles",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "j_quote_items",
                columns: table => new
                {
                    id = table.Column<string>(type: "character(36)", unicode: false, fixedLength: true, maxLength: 36, nullable: false),
                    quoteid = table.Column<string>(name: "quote_id", type: "character(36)", unicode: false, fixedLength: true, maxLength: 36, nullable: false),
                    sortorder = table.Column<int>(name: "sort_order", type: "integer", nullable: false),
                    jobcode = table.Column<string>(name: "job_code", type: "character varying(10)", maxLength: 10, nullable: true),
                    description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    location = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    labourhours = table.Column<int>(name: "labour_hours", type: "integer", nullable: false),
                    labourrate = table.Column<int>(name: "labour_rate", type: "integer", nullable: false),
                    partrate = table.Column<int>(name: "part_rate", type: "integer", nullable: false),
                    partmarkup = table.Column<int>(name: "part_markup", type: "integer", nullable: false),
                    partquantity = table.Column<int>(name: "part_quantity", type: "integer", nullable: false),
                    auth = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_j_quote_items_1", x => x.id);
                    table.ForeignKey(
                        name: "FK_items",
                        column: x => x.quoteid,
                        principalTable: "j_quotes",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "j_quote_item_supplier",
                columns: table => new
                {
                    id = table.Column<string>(type: "character(36)", unicode: false, fixedLength: true, maxLength: 36, nullable: false),
                    supplierid = table.Column<string>(name: "supplier_id", type: "character(36)", unicode: false, fixedLength: true, maxLength: 36, nullable: false),
                    quoteitemid = table.Column<string>(name: "quote_item_id", type: "character(36)", unicode: false, fixedLength: true, maxLength: 36, nullable: false),
                    quotedprice = table.Column<int>(name: "quoted_price", type: "integer", nullable: false),
                    partnumber = table.Column<string>(name: "part_number", type: "character varying(100)", maxLength: 100, nullable: true),
                    count = table.Column<int>(type: "integer", nullable: false),
                    quotedby = table.Column<string>(name: "quoted_by", type: "character varying(100)", maxLength: 100, nullable: false),
                    quoteddatetime = table.Column<DateTime>(name: "quoted_datetime", type: "timestamp with time zone", nullable: false),
                    accepteddatetime = table.Column<DateTime>(name: "accepted_datetime", type: "timestamp with time zone", nullable: true),
                    acceptedbyuserid = table.Column<string>(name: "accepted_by_user_id", type: "character(36)", unicode: false, fixedLength: true, maxLength: 36, nullable: true),
                    remarks = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_j_quote_item_quotes", x => x.id);
                    table.ForeignKey(
                        name: "FK_j_quote_item_supplier_j_quote_items",
                        column: x => x.quoteitemid,
                        principalTable: "j_quote_items",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_j_quote_item_supplier_j_users",
                        column: x => x.acceptedbyuserid,
                        principalTable: "j_users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_supplier",
                        column: x => x.supplierid,
                        principalTable: "j_supplier_branches",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_j_quote_item_supplier_accepted_by_user_id",
                table: "j_quote_item_supplier",
                column: "accepted_by_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_j_quote_item_supplier_quote_item_id",
                table: "j_quote_item_supplier",
                column: "quote_item_id");

            migrationBuilder.CreateIndex(
                name: "IX_j_quote_item_supplier_supplier_id",
                table: "j_quote_item_supplier",
                column: "supplier_id");

            migrationBuilder.CreateIndex(
                name: "IX_j_quote_items_quote_id",
                table: "j_quote_items",
                column: "quote_id");

            migrationBuilder.CreateIndex(
                name: "IX_j_quotes_create_user_id",
                table: "j_quotes",
                column: "create_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_j_quotes_customer_id",
                table: "j_quotes",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_j_quotes_tech_id",
                table: "j_quotes",
                column: "tech_id");

            migrationBuilder.CreateIndex(
                name: "IX_j_quotes_update_user_id",
                table: "j_quotes",
                column: "update_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_j_quotes_vehicle_id",
                table: "j_quotes",
                column: "vehicle_id");

            migrationBuilder.CreateIndex(
                name: "IX_j_supplier_branches_supplier_id",
                table: "j_supplier_branches",
                column: "supplier_id");

            migrationBuilder.CreateIndex(
                name: "IX_j_user_site_access_site_id",
                table: "j_user_site_access",
                column: "site_id");

            migrationBuilder.CreateIndex(
                name: "IX_j_users_1",
                table: "j_users",
                column: "username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "j_job_codes");

            migrationBuilder.DropTable(
                name: "j_quote_item_supplier");

            migrationBuilder.DropTable(
                name: "j_quote_status");

            migrationBuilder.DropTable(
                name: "j_user_site_access");

            migrationBuilder.DropTable(
                name: "j_vehicle_models");

            migrationBuilder.DropTable(
                name: "j_quote_items");

            migrationBuilder.DropTable(
                name: "j_supplier_branches");

            migrationBuilder.DropTable(
                name: "j_sites");

            migrationBuilder.DropTable(
                name: "j_quotes");

            migrationBuilder.DropTable(
                name: "j_suppliers");

            migrationBuilder.DropTable(
                name: "j_users");

            migrationBuilder.DropTable(
                name: "j_customers");

            migrationBuilder.DropTable(
                name: "j_techs");

            migrationBuilder.DropTable(
                name: "j_vehicles");
        }
    }
}
