IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [j_customers] (
    [id] char(36) NOT NULL,
    [title] nvarchar(20) NOT NULL,
    [name] nvarchar(50) NOT NULL,
    [surname] nvarchar(50) NOT NULL,
    [mobile_no] varchar(12) NOT NULL,
    [home_no] varchar(12) NULL,
    [work_no] nvarchar(50) NULL,
    [alt_no] nvarchar(50) NULL,
    [email] nvarchar(50) NOT NULL,
    [type] nvarchar(10) NULL,
    [reg_number] nvarchar(50) NULL,
    [vat_no] nvarchar(50) NULL,
    [company_name] nvarchar(100) NULL,
    [address_line_1] nvarchar(50) NULL,
    [address_line_2] nvarchar(50) NULL,
    [address_line_3] nvarchar(50) NULL,
    [postal] nvarchar(50) NULL,
    [site_access_id] char(36) NULL,
    CONSTRAINT [PK_j_customers] PRIMARY KEY ([id])
);
GO

CREATE TABLE [j_errors] (
    [error_id] char(36) NOT NULL,
    [date] datetime NOT NULL,
    [data] text NOT NULL,
    CONSTRAINT [PK_j_errors] PRIMARY KEY ([error_id])
);
GO

CREATE TABLE [j_job_codes] (
    [code] nvarchar(50) NOT NULL,
    [site_access_id] char(36) NOT NULL,
    [description] nvarchar(255) NOT NULL,
    [location] nvarchar(50) NULL,
    [cost] int NOT NULL,
    [markup] int NOT NULL,
    [standard_hours] int NOT NULL,
    [standard_volume] int NOT NULL,
    [labour_rate] int NOT NULL,
    CONSTRAINT [PK_j_job_codes] PRIMARY KEY ([code], [site_access_id])
);
GO

CREATE TABLE [j_quote_status] (
    [status] nvarchar(50) NOT NULL,
    [sort_order] int NOT NULL,
    CONSTRAINT [PK_j_quote_status] PRIMARY KEY ([status])
);
GO

CREATE TABLE [j_sites] (
    [id] char(36) NOT NULL,
    [description] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_j_sites] PRIMARY KEY ([id])
);
GO

CREATE TABLE [j_suppliers] (
    [id] char(36) NOT NULL,
    [name] nvarchar(50) NOT NULL,
    [reg_no] nvarchar(50) NULL,
    [vat_no] nvarchar(50) NULL,
    [tax_clearance] nvarchar(50) NULL,
    [credit_limit] int NULL,
    [credit_balance] int NULL,
    [tel_num] nvarchar(12) NULL,
    [address_line_1] nvarchar(50) NULL,
    [address_line_2] nvarchar(50) NULL,
    [address_line_3] nvarchar(50) NULL,
    [postal] nvarchar(10) NULL,
    [contact_person] nvarchar(50) NULL,
    [contact_no] nvarchar(12) NULL,
    [email] nvarchar(320) NULL,
    [after_hours_no] nvarchar(12) NULL,
    [standby_person] nvarchar(50) NULL,
    [standby_no] nvarchar(12) NULL,
    [standby_email] nvarchar(320) NULL,
    CONSTRAINT [PK_j_suppliers_1] PRIMARY KEY ([id])
);
GO

CREATE TABLE [j_techs] (
    [id] char(36) NOT NULL,
    [name] nvarchar(30) NOT NULL,
    [surname] nvarchar(30) NOT NULL,
    [site_access_id] char(36) NULL,
    CONSTRAINT [PK_j_techs] PRIMARY KEY ([id])
);
GO

CREATE TABLE [j_users] (
    [id] char(36) NOT NULL,
    [username] nvarchar(30) NOT NULL,
    [name] nvarchar(50) NOT NULL,
    [surname] nvarchar(50) NOT NULL,
    [password] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CS_AS NOT NULL,
    [tel_no] varchar(50) NULL,
    [active] bit NOT NULL,
    [password_date] date NOT NULL,
    [end_date] date NOT NULL,
    [role] varchar(50) NOT NULL,
    CONSTRAINT [PK_j_users_1] PRIMARY KEY ([id])
);
GO

CREATE TABLE [j_vehicle_models] (
    [brand] nvarchar(50) NOT NULL,
    [model] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_j_vehicle_models] PRIMARY KEY ([brand], [model])
);
GO

CREATE TABLE [j_vehicles] (
    [id] char(36) NOT NULL,
    [customer_id] char(36) NOT NULL,
    [vin_number] varchar(20) NOT NULL,
    [engine_number] varchar(20) NOT NULL,
    [registration] nvarchar(15) NOT NULL,
    [brand] nvarchar(50) NOT NULL,
    [model] nvarchar(50) NOT NULL,
    [year] int NOT NULL,
    [site_access_id] char(36) NULL,
    CONSTRAINT [PK_j_vehicles] PRIMARY KEY ([id])
);
GO

CREATE TABLE [j_supplier_branches] (
    [id] char(36) NOT NULL,
    [supplier_id] char(36) NULL,
    [name] nvarchar(50) NOT NULL,
    [address_line_1] nvarchar(50) NULL,
    [address_line_2] nvarchar(50) NULL,
    [address_line_3] nvarchar(50) NULL,
    [postal] nvarchar(10) NULL,
    [lat] decimal(16,8) NULL,
    [lgn] decimal(16,8) NULL,
    [contact_person] nvarchar(50) NULL,
    [contact_number] nvarchar(50) NULL,
    [email] nvarchar(320) NULL,
    CONSTRAINT [PK_j_suppliers] PRIMARY KEY ([id]),
    CONSTRAINT [FK_j_supplier_branches_j_suppliers] FOREIGN KEY ([supplier_id]) REFERENCES [j_suppliers] ([id])
);
GO

CREATE TABLE [j_user_site_access] (
    [user_id] char(36) NOT NULL,
    [site_id] char(36) NOT NULL,
    CONSTRAINT [PK_j_user_site_access] PRIMARY KEY ([user_id], [site_id]),
    CONSTRAINT [FK_j_user_site_access_j_sites] FOREIGN KEY ([site_id]) REFERENCES [j_sites] ([id]),
    CONSTRAINT [FK_j_user_site_access_j_user_site_access] FOREIGN KEY ([user_id]) REFERENCES [j_users] ([id])
);
GO

CREATE TABLE [j_quotes] (
    [id] char(36) NOT NULL,
    [ro_number] nvarchar(20) NOT NULL,
    [branch_id] char(36) NOT NULL,
    [customer_id] char(36) NOT NULL,
    [vehicle_id] char(36) NOT NULL,
    [create_user_id] char(36) NOT NULL,
    [create_datetime] datetime NOT NULL,
    [update_user_id] char(36) NOT NULL,
    [update_datetime] datetime NOT NULL,
    [status] nvarchar(50) NOT NULL,
    [checkin_odometer] int NOT NULL,
    [tech_id] char(36) NULL,
    [site_access] char(36) NULL,
    CONSTRAINT [PK_j_quotes] PRIMARY KEY ([id]),
    CONSTRAINT [FK_create_user] FOREIGN KEY ([create_user_id]) REFERENCES [j_users] ([id]),
    CONSTRAINT [FK_customer] FOREIGN KEY ([customer_id]) REFERENCES [j_customers] ([id]),
    CONSTRAINT [FK_tech] FOREIGN KEY ([tech_id]) REFERENCES [j_techs] ([id]),
    CONSTRAINT [FK_update_user] FOREIGN KEY ([update_user_id]) REFERENCES [j_users] ([id]),
    CONSTRAINT [FK_vehicle] FOREIGN KEY ([vehicle_id]) REFERENCES [j_vehicles] ([id])
);
GO

CREATE TABLE [j_quote_items] (
    [id] char(36) NOT NULL,
    [quote_id] char(36) NOT NULL,
    [sort_order] int NOT NULL,
    [job_code] nvarchar(10) NULL,
    [description] nvarchar(255) NOT NULL,
    [location] nvarchar(50) NULL,
    [labour_hours] int NOT NULL,
    [labour_rate] int NOT NULL,
    [part_rate] int NOT NULL,
    [part_markup] int NOT NULL,
    [part_quantity] int NOT NULL,
    [auth] bit NOT NULL,
    CONSTRAINT [PK_j_quote_items_1] PRIMARY KEY ([id]),
    CONSTRAINT [FK_items] FOREIGN KEY ([quote_id]) REFERENCES [j_quotes] ([id])
);
GO

CREATE TABLE [j_quote_item_supplier] (
    [id] char(36) NOT NULL,
    [supplier_id] char(36) NOT NULL,
    [quote_item_id] char(36) NOT NULL,
    [quoted_price] int NOT NULL,
    [part_number] nvarchar(100) NULL,
    [count] int NOT NULL,
    [quoted_by] nvarchar(100) NOT NULL,
    [quoted_datetime] datetime2 NOT NULL,
    [accepted_datetime] datetime2 NULL,
    [accepted_by_user_id] char(36) NULL,
    [remarks] text NULL,
    CONSTRAINT [PK_j_quote_item_quotes] PRIMARY KEY ([id]),
    CONSTRAINT [FK_j_quote_item_supplier_j_quote_items] FOREIGN KEY ([quote_item_id]) REFERENCES [j_quote_items] ([id]),
    CONSTRAINT [FK_j_quote_item_supplier_j_users] FOREIGN KEY ([accepted_by_user_id]) REFERENCES [j_users] ([id]),
    CONSTRAINT [FK_supplier] FOREIGN KEY ([supplier_id]) REFERENCES [j_supplier_branches] ([id])
);
GO

CREATE INDEX [IX_j_quote_item_supplier_accepted_by_user_id] ON [j_quote_item_supplier] ([accepted_by_user_id]);
GO

CREATE INDEX [IX_j_quote_item_supplier_quote_item_id] ON [j_quote_item_supplier] ([quote_item_id]);
GO

CREATE INDEX [IX_j_quote_item_supplier_supplier_id] ON [j_quote_item_supplier] ([supplier_id]);
GO

CREATE INDEX [IX_j_quote_items_quote_id] ON [j_quote_items] ([quote_id]);
GO

CREATE INDEX [IX_j_quotes_create_user_id] ON [j_quotes] ([create_user_id]);
GO

CREATE INDEX [IX_j_quotes_customer_id] ON [j_quotes] ([customer_id]);
GO

CREATE INDEX [IX_j_quotes_tech_id] ON [j_quotes] ([tech_id]);
GO

CREATE INDEX [IX_j_quotes_update_user_id] ON [j_quotes] ([update_user_id]);
GO

CREATE INDEX [IX_j_quotes_vehicle_id] ON [j_quotes] ([vehicle_id]);
GO

CREATE INDEX [IX_j_supplier_branches_supplier_id] ON [j_supplier_branches] ([supplier_id]);
GO

CREATE INDEX [IX_j_user_site_access_site_id] ON [j_user_site_access] ([site_id]);
GO

CREATE UNIQUE INDEX [IX_j_users_1] ON [j_users] ([username]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221221074735_InitialCreate', N'7.0.1');
GO

COMMIT;
GO

