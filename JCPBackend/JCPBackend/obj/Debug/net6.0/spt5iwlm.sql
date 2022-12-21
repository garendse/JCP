CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE TABLE j_customers (
    id character(36) NOT NULL,
    title character varying(20) NOT NULL,
    name character varying(50) NOT NULL,
    surname character varying(50) NOT NULL,
    mobile_no character varying(12) NOT NULL,
    home_no character varying(12) NULL,
    work_no character varying(50) NULL,
    alt_no character varying(50) NULL,
    email character varying(50) NOT NULL,
    type character varying(10) NULL,
    reg_number character varying(50) NULL,
    vat_no character varying(50) NULL,
    company_name character varying(100) NULL,
    address_line_1 character varying(50) NULL,
    address_line_2 character varying(50) NULL,
    address_line_3 character varying(50) NULL,
    postal character varying(50) NULL,
    site_access_id character(36) NULL,
    CONSTRAINT "PK_j_customers" PRIMARY KEY (id)
);

CREATE TABLE j_errors (
    error_id character(36) NOT NULL,
    date datetime NOT NULL,
    data text NOT NULL,
    CONSTRAINT "PK_j_errors" PRIMARY KEY (error_id)
);

CREATE TABLE j_job_codes (
    code character varying(50) NOT NULL,
    site_access_id character(36) NOT NULL,
    description character varying(255) NOT NULL,
    location character varying(50) NULL,
    cost integer NOT NULL,
    markup integer NOT NULL,
    standard_hours integer NOT NULL,
    standard_volume integer NOT NULL,
    labour_rate integer NOT NULL,
    CONSTRAINT "PK_j_job_codes" PRIMARY KEY (code, site_access_id)
);

CREATE TABLE j_quote_status (
    status character varying(50) NOT NULL,
    sort_order integer NOT NULL,
    CONSTRAINT "PK_j_quote_status" PRIMARY KEY (status)
);

CREATE TABLE j_sites (
    id character(36) NOT NULL,
    description character varying(50) NOT NULL,
    CONSTRAINT "PK_j_sites" PRIMARY KEY (id)
);

CREATE TABLE j_suppliers (
    id character(36) NOT NULL,
    name character varying(50) NOT NULL,
    reg_no character varying(50) NULL,
    vat_no character varying(50) NULL,
    tax_clearance character varying(50) NULL,
    credit_limit integer NULL,
    credit_balance integer NULL,
    tel_num character varying(12) NULL,
    address_line_1 character varying(50) NULL,
    address_line_2 character varying(50) NULL,
    address_line_3 character varying(50) NULL,
    postal character varying(10) NULL,
    contact_person character varying(50) NULL,
    contact_no character varying(12) NULL,
    email character varying(320) NULL,
    after_hours_no character varying(12) NULL,
    standby_person character varying(50) NULL,
    standby_no character varying(12) NULL,
    standby_email character varying(320) NULL,
    CONSTRAINT "PK_j_suppliers_1" PRIMARY KEY (id)
);

CREATE TABLE j_techs (
    id character(36) NOT NULL,
    name character varying(30) NOT NULL,
    surname character varying(30) NOT NULL,
    site_access_id character(36) NULL,
    CONSTRAINT "PK_j_techs" PRIMARY KEY (id)
);

CREATE TABLE j_users (
    id character(36) NOT NULL,
    username character varying(30) NOT NULL,
    name character varying(50) NOT NULL,
    surname character varying(50) NOT NULL,
    password character varying(50) COLLATE "SQL_Latin1_General_CP1_CS_AS" NOT NULL,
    tel_no character varying(50) NULL,
    active boolean NOT NULL,
    password_date date NOT NULL,
    end_date date NOT NULL,
    role character varying(50) NOT NULL,
    CONSTRAINT "PK_j_users_1" PRIMARY KEY (id)
);

CREATE TABLE j_vehicle_models (
    brand character varying(50) NOT NULL,
    model character varying(50) NOT NULL,
    CONSTRAINT "PK_j_vehicle_models" PRIMARY KEY (brand, model)
);

CREATE TABLE j_vehicles (
    id character(36) NOT NULL,
    customer_id character(36) NOT NULL,
    vin_number character varying(20) NOT NULL,
    engine_number character varying(20) NOT NULL,
    registration character varying(15) NOT NULL,
    brand character varying(50) NOT NULL,
    model character varying(50) NOT NULL,
    year integer NOT NULL,
    site_access_id character(36) NULL,
    CONSTRAINT "PK_j_vehicles" PRIMARY KEY (id)
);

CREATE TABLE j_supplier_branches (
    id character(36) NOT NULL,
    supplier_id character(36) NULL,
    name character varying(50) NOT NULL,
    address_line_1 character varying(50) NULL,
    address_line_2 character varying(50) NULL,
    address_line_3 character varying(50) NULL,
    postal character varying(10) NULL,
    lat numeric(16,8) NULL,
    lgn numeric(16,8) NULL,
    contact_person character varying(50) NULL,
    contact_number character varying(50) NULL,
    email character varying(320) NULL,
    CONSTRAINT "PK_j_suppliers" PRIMARY KEY (id),
    CONSTRAINT "FK_j_supplier_branches_j_suppliers" FOREIGN KEY (supplier_id) REFERENCES j_suppliers (id)
);

CREATE TABLE j_user_site_access (
    user_id character(36) NOT NULL,
    site_id character(36) NOT NULL,
    CONSTRAINT "PK_j_user_site_access" PRIMARY KEY (user_id, site_id),
    CONSTRAINT "FK_j_user_site_access_j_sites" FOREIGN KEY (site_id) REFERENCES j_sites (id),
    CONSTRAINT "FK_j_user_site_access_j_user_site_access" FOREIGN KEY (user_id) REFERENCES j_users (id)
);

CREATE TABLE j_quotes (
    id character(36) NOT NULL,
    ro_number character varying(20) NOT NULL,
    branch_id character(36) NOT NULL,
    customer_id character(36) NOT NULL,
    vehicle_id character(36) NOT NULL,
    create_user_id character(36) NOT NULL,
    create_datetime datetime NOT NULL,
    update_user_id character(36) NOT NULL,
    update_datetime datetime NOT NULL,
    status character varying(50) NOT NULL,
    checkin_odometer integer NOT NULL,
    tech_id character(36) NULL,
    site_access character(36) NULL,
    CONSTRAINT "PK_j_quotes" PRIMARY KEY (id),
    CONSTRAINT "FK_create_user" FOREIGN KEY (create_user_id) REFERENCES j_users (id),
    CONSTRAINT "FK_customer" FOREIGN KEY (customer_id) REFERENCES j_customers (id),
    CONSTRAINT "FK_tech" FOREIGN KEY (tech_id) REFERENCES j_techs (id),
    CONSTRAINT "FK_update_user" FOREIGN KEY (update_user_id) REFERENCES j_users (id),
    CONSTRAINT "FK_vehicle" FOREIGN KEY (vehicle_id) REFERENCES j_vehicles (id)
);

CREATE TABLE j_quote_items (
    id character(36) NOT NULL,
    quote_id character(36) NOT NULL,
    sort_order integer NOT NULL,
    job_code character varying(10) NULL,
    description character varying(255) NOT NULL,
    location character varying(50) NULL,
    labour_hours integer NOT NULL,
    labour_rate integer NOT NULL,
    part_rate integer NOT NULL,
    part_markup integer NOT NULL,
    part_quantity integer NOT NULL,
    auth boolean NOT NULL,
    CONSTRAINT "PK_j_quote_items_1" PRIMARY KEY (id),
    CONSTRAINT "FK_items" FOREIGN KEY (quote_id) REFERENCES j_quotes (id)
);

CREATE TABLE j_quote_item_supplier (
    id character(36) NOT NULL,
    supplier_id character(36) NOT NULL,
    quote_item_id character(36) NOT NULL,
    quoted_price integer NOT NULL,
    part_number character varying(100) NULL,
    count integer NOT NULL,
    quoted_by character varying(100) NOT NULL,
    quoted_datetime timestamp with time zone NOT NULL,
    accepted_datetime timestamp with time zone NULL,
    accepted_by_user_id character(36) NULL,
    remarks text NULL,
    CONSTRAINT "PK_j_quote_item_quotes" PRIMARY KEY (id),
    CONSTRAINT "FK_j_quote_item_supplier_j_quote_items" FOREIGN KEY (quote_item_id) REFERENCES j_quote_items (id),
    CONSTRAINT "FK_j_quote_item_supplier_j_users" FOREIGN KEY (accepted_by_user_id) REFERENCES j_users (id),
    CONSTRAINT "FK_supplier" FOREIGN KEY (supplier_id) REFERENCES j_supplier_branches (id)
);

CREATE INDEX "IX_j_quote_item_supplier_accepted_by_user_id" ON j_quote_item_supplier (accepted_by_user_id);

CREATE INDEX "IX_j_quote_item_supplier_quote_item_id" ON j_quote_item_supplier (quote_item_id);

CREATE INDEX "IX_j_quote_item_supplier_supplier_id" ON j_quote_item_supplier (supplier_id);

CREATE INDEX "IX_j_quote_items_quote_id" ON j_quote_items (quote_id);

CREATE INDEX "IX_j_quotes_create_user_id" ON j_quotes (create_user_id);

CREATE INDEX "IX_j_quotes_customer_id" ON j_quotes (customer_id);

CREATE INDEX "IX_j_quotes_tech_id" ON j_quotes (tech_id);

CREATE INDEX "IX_j_quotes_update_user_id" ON j_quotes (update_user_id);

CREATE INDEX "IX_j_quotes_vehicle_id" ON j_quotes (vehicle_id);

CREATE INDEX "IX_j_supplier_branches_supplier_id" ON j_supplier_branches (supplier_id);

CREATE INDEX "IX_j_user_site_access_site_id" ON j_user_site_access (site_id);

CREATE UNIQUE INDEX "IX_j_users_1" ON j_users (username);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20221221085357_InitialCreate', '7.0.1');

COMMIT;

