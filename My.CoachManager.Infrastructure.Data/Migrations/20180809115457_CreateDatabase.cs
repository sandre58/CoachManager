using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace My.CoachManager.Infrastructure.Data.Migrations
{
    public partial class CreateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false),
                    Code = table.Column<string>(maxLength: 15, nullable: false),
                    Label = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Year = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.UniqueConstraint("AK_Categories_Code", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false),
                    Code = table.Column<string>(maxLength: 15, nullable: false),
                    Label = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Flag = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                    table.UniqueConstraint("AK_Countries_Code", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Seasons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false),
                    Code = table.Column<string>(maxLength: 15, nullable: false),
                    Label = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seasons", x => x.Id);
                    table.UniqueConstraint("AK_Seasons_Code", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 150, nullable: false),
                    Login = table.Column<string>(maxLength: 100, nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Mail = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Adresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    Row1 = table.Column<string>(nullable: false),
                    Row2 = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(maxLength: 5, nullable: false),
                    City = table.Column<string>(nullable: false),
                    CountryId = table.Column<int>(nullable: true),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Adresses_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Rosters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    SeasonId = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rosters", x => x.Id);
                    table.UniqueConstraint("AK_Rosters_SeasonId_CategoryId", x => new { x.SeasonId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_Rosters_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rosters_Seasons_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Seasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    Birthdate = table.Column<DateTime>(nullable: true),
                    PlaceOfBirth = table.Column<string>(nullable: true),
                    CountryId = table.Column<int>(nullable: true),
                    Photo = table.Column<byte[]>(nullable: true),
                    Gender = table.Column<int>(nullable: false, defaultValue: 0),
                    AddressId = table.Column<int>(nullable: true),
                    LicenseNumber = table.Column<string>(maxLength: 10, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Size = table.Column<string>(maxLength: 4, nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    CategoryId = table.Column<int>(nullable: true),
                    Laterality = table.Column<int>(nullable: true, defaultValue: 1),
                    Height = table.Column<int>(nullable: true),
                    Weight = table.Column<int>(nullable: true),
                    ShoesSize = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Persons_Adresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Adresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Persons_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Persons_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    Label = table.Column<string>(nullable: true),
                    Default = table.Column<bool>(nullable: false),
                    Value = table.Column<string>(nullable: false),
                    PersonId = table.Column<int>(nullable: false),
                    PersonId1 = table.Column<int>(nullable: true),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacts_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contacts_Persons_PersonId1",
                        column: x => x.PersonId1,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RosterPlayers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    RosterId = table.Column<int>(nullable: false),
                    PlayerId = table.Column<int>(nullable: false),
                    Number = table.Column<int>(nullable: true),
                    LicenseState = table.Column<int>(nullable: false, defaultValue: 0),
                    IsMutation = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RosterPlayers", x => new { x.RosterId, x.PlayerId });
                    table.ForeignKey(
                        name: "FK_RosterPlayers_Persons_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RosterPlayers_Rosters_RosterId",
                        column: x => x.RosterId,
                        principalTable: "Rosters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Code", "CreatedBy", "CreatedDate", "Description", "Label", "ModifiedBy", "ModifiedDate", "Order", "Year" },
                values: new object[,]
                {
                    { 1, "S", null, null, null, "Séniors", null, null, 1, 1998 },
                    { 16, "U6", null, null, null, "U6", null, null, 16, 2012 },
                    { 14, "U8", null, null, null, "U8", null, null, 14, 2010 },
                    { 13, "U9", null, null, null, "U9", null, null, 13, 2009 },
                    { 12, "U10", null, null, null, "U10", null, null, 12, 2008 },
                    { 11, "U11", null, null, null, "U11", null, null, 11, 2007 },
                    { 10, "U12", null, null, null, "U12", null, null, 10, 2006 },
                    { 9, "U13", null, null, null, "U13", null, null, 9, 2005 },
                    { 15, "U7", null, null, null, "U7", null, null, 15, 2011 },
                    { 7, "U15", null, null, null, "U15", null, null, 7, 2003 },
                    { 6, "U16", null, null, null, "U16", null, null, 6, 2002 },
                    { 5, "U17", null, null, null, "U17", null, null, 5, 2001 },
                    { 4, "U18", null, null, null, "U18", null, null, 4, 2000 },
                    { 3, "U19", null, null, null, "U19", null, null, 3, 1999 },
                    { 2, "V", null, null, null, "Vétérans", null, null, 2, 1982 },
                    { 8, "U14", null, null, null, "U14", null, null, 8, 2004 }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "CreatedBy", "CreatedDate", "Description", "Flag", "Label", "ModifiedBy", "ModifiedDate", "Order" },
                values: new object[,]
                {
                    { 170, "pse", null, null, null, "ps.png", "Palestinian Territory, Occupied", null, null, 0 },
                    { 169, "plw", null, null, null, "pw.png", "Palau", null, null, 0 },
                    { 168, "pak", null, null, null, "pk.png", "Pakistan", null, null, 0 },
                    { 167, "omn", null, null, null, "om.png", "Oman", null, null, 0 },
                    { 166, "nor", null, null, null, "no.png", "Norway", null, null, 0 },
                    { 162, "nga", null, null, null, "ng.png", "Nigeria", null, null, 0 },
                    { 164, "nfk", null, null, null, "nf.png", "Norfolk Island", null, null, 0 },
                    { 163, "niu", null, null, null, "nu.png", "Niue", null, null, 0 },
                    { 161, "ner", null, null, null, "ne.png", "Niger", null, null, 0 },
                    { 160, "nic", null, null, null, "ni.png", "Nicaragua", null, null, 0 },
                    { 171, "pan", null, null, null, "pa.png", "Panama", null, null, 0 },
                    { 165, "mnp", null, null, null, "mp.png", "Northern Mariana Islands", null, null, 0 },
                    { 172, "png", null, null, null, "pg.png", "Papua New Guinea", null, null, 0 },
                    { 180, "qat", null, null, null, "qa.png", "Qatar", null, null, 0 },
                    { 174, "per", null, null, null, "pe.png", "Peru", null, null, 0 },
                    { 175, "phl", null, null, null, "ph.png", "Philippines", null, null, 0 },
                    { 176, "pcn", null, null, null, "pn.png", "Pitcairn", null, null, 0 },
                    { 177, "pol", null, null, null, "pl.png", "Poland", null, null, 0 },
                    { 178, "prt", null, null, null, "pt.png", "Portugal", null, null, 0 },
                    { 179, "pri", null, null, null, "pr.png", "Puerto Rico", null, null, 0 },
                    { 159, "nzl", null, null, null, "nz.png", "New Zealand", null, null, 0 },
                    { 181, "reu", null, null, null, "re.png", "Reunion", null, null, 0 },
                    { 182, "rou", null, null, null, "ro.png", "Romania", null, null, 0 },
                    { 183, "rus", null, null, null, "ru.png", "Russian Federation", null, null, 0 },
                    { 184, "rwa", null, null, null, "rw.png", "Rwanda", null, null, 0 },
                    { 185, "blm", null, null, null, "bl.png", "Saint Barthelemy", null, null, 0 },
                    { 186, "shn", null, null, null, "sh.png", "Saint Helena, Ascension and Tristan Da Cunha", null, null, 0 },
                    { 173, "pry", null, null, null, "py.png", "Paraguay", null, null, 0 },
                    { 158, "ncl", null, null, null, "nc.png", "New Caledonia", null, null, 0 },
                    { 150, "msr", null, null, null, "ms.png", "Montserrat", null, null, 0 },
                    { 156, "npl", null, null, null, "np.png", "Nepal", null, null, 0 },
                    { 128, "lie", null, null, null, "li.png", "Liechtenstein", null, null, 0 },
                    { 129, "ltu", null, null, null, "lt.png", "Lithuania", null, null, 0 },
                    { 130, "lux", null, null, null, "lu.png", "Luxembourg", null, null, 0 },
                    { 131, "mac", null, null, null, "mo.png", "Macao", null, null, 0 },
                    { 132, "mkd", null, null, null, "mk.png", "Macedonia, The former Yugoslav Republic of", null, null, 0 },
                    { 133, "mdg", null, null, null, "mg.png", "Madagascar", null, null, 0 },
                    { 134, "mwi", null, null, null, "mw.png", "Malawi", null, null, 0 },
                    { 135, "mys", null, null, null, "my.png", "Malaysia", null, null, 0 },
                    { 136, "mdv", null, null, null, "mv.png", "Maldives", null, null, 0 },
                    { 137, "mli", null, null, null, "ml.png", "Mali", null, null, 0 },
                    { 138, "mlt", null, null, null, "mt.png", "Malta", null, null, 0 },
                    { 139, "mhl", null, null, null, "mh.png", "Marshall Islands", null, null, 0 },
                    { 140, "mtq", null, null, null, "mq.png", "Martinique", null, null, 0 },
                    { 157, "nld", null, null, null, "nl.png", "Netherlands", null, null, 0 },
                    { 141, "mrt", null, null, null, "mr.png", "Mauritania", null, null, 0 },
                    { 143, "myt", null, null, null, "yt.png", "Mayotte", null, null, 0 },
                    { 144, "mex", null, null, null, "mx.png", "Mexico", null, null, 0 },
                    { 145, "fsm", null, null, null, "fm.png", "Micronesia, Federated States of", null, null, 0 },
                    { 146, "mda", null, null, null, "md.png", "Moldova, Republic of", null, null, 0 },
                    { 147, "mco", null, null, null, "mc.png", "Monaco", null, null, 0 },
                    { 148, "mng", null, null, null, "mn.png", "Mongolia", null, null, 0 },
                    { 149, "mne", null, null, null, "me.png", "Montenegro", null, null, 0 },
                    { 187, "kna", null, null, null, "kn.png", "Saint Kitts and Nevis", null, null, 0 },
                    { 151, "mar", null, null, null, "ma.png", "Morocco", null, null, 0 },
                    { 152, "moz", null, null, null, "mz.png", "Mozambique", null, null, 0 },
                    { 153, "mmr", null, null, null, "mm.png", "Myanmar", null, null, 0 },
                    { 154, "nam", null, null, null, "na.png", "Namibia", null, null, 0 },
                    { 155, "nru", null, null, null, "nr.png", "Nauru", null, null, 0 },
                    { 142, "mus", null, null, null, "mu.png", "Mauritius", null, null, 0 },
                    { 188, "lca", null, null, null, "lc.png", "Saint Lucia", null, null, 0 },
                    { 196, "sen", null, null, null, "sn.png", "Senegal", null, null, 0 },
                    { 190, "spm", null, null, null, "pm.png", "Saint Pierre and Miquelon", null, null, 0 },
                    { 223, "tgo", null, null, null, "tg.png", "Togo", null, null, 0 },
                    { 224, "tkl", null, null, null, "tk.png", "Tokelau", null, null, 0 },
                    { 225, "ton", null, null, null, "to.png", "Tonga", null, null, 0 },
                    { 226, "tto", null, null, null, "tt.png", "Trinidad and Tobago", null, null, 0 },
                    { 227, "tun", null, null, null, "tn.png", "Tunisia", null, null, 0 },
                    { 228, "tur", null, null, null, "tr.png", "Turkey", null, null, 0 },
                    { 229, "tkm", null, null, null, "tm.png", "Turkmenistan", null, null, 0 },
                    { 230, "tca", null, null, null, "tc.png", "Turks and Caicos Islands", null, null, 0 },
                    { 231, "tuv", null, null, null, "tv.png", "Tuvalu", null, null, 0 },
                    { 232, "uga", null, null, null, "ug.png", "Uganda", null, null, 0 },
                    { 233, "ukr", null, null, null, "ua.png", "Ukraine", null, null, 0 },
                    { 234, "are", null, null, null, "ae.png", "United Arab Emirates", null, null, 0 },
                    { 235, "gbr", null, null, null, "gb.png", "United Kingdom", null, null, 0 },
                    { 236, "usa", null, null, null, "us.png", "United States", null, null, 0 },
                    { 237, "usm", null, null, null, "um.png", "United States Minor Outlying Islands", null, null, 0 },
                    { 238, "ury", null, null, null, "uy.png", "Uruguay", null, null, 0 },
                    { 239, "uzb", null, null, null, "uz.png", "Uzbekistan", null, null, 0 },
                    { 240, "vut", null, null, null, "vu.png", "Vanuatu", null, null, 0 },
                    { 241, "ven", null, null, null, "ve.png", "Venezuela, Bolivarian Republic of", null, null, 0 },
                    { 242, "vnm", null, null, null, "vn.png", "Viet Nam", null, null, 0 },
                    { 243, "vgb", null, null, null, "vg.png", "Virgin Islands, British", null, null, 0 },
                    { 244, "vir", null, null, null, "vi.png", "Virgin Islands, U.S.", null, null, 0 },
                    { 245, "wlf", null, null, null, "wf.png", "Wallis and Futuna", null, null, 0 },
                    { 246, "esh", null, null, null, "eh.png", "Western Sahara", null, null, 0 },
                    { 247, "yem", null, null, null, "ye.png", "Yemen", null, null, 0 },
                    { 248, "zmb", null, null, null, "zm.png", "Zambia", null, null, 0 },
                    { 249, "zwe", null, null, null, "zw.png", "Zimbabwe", null, null, 0 },
                    { 222, "tls", null, null, null, "tl.png", "Timor-Leste", null, null, 0 },
                    { 221, "tha", null, null, null, "th.png", "Thailand", null, null, 0 },
                    { 220, "tza", null, null, null, "tz.png", "Tanzania, United Republic of", null, null, 0 },
                    { 219, "tjk", null, null, null, "tj.png", "Tajikistan", null, null, 0 },
                    { 191, "vct", null, null, null, "vc.png", "Saint Vincent and The Grenadines", null, null, 0 },
                    { 192, "wsm", null, null, null, "ws.png", "Samoa", null, null, 0 },
                    { 193, "smr", null, null, null, "sm.png", "San Marino", null, null, 0 },
                    { 194, "stp", null, null, null, "st.png", "Sao Tome and Principe", null, null, 0 },
                    { 195, "sau", null, null, null, "sa.png", "Saudi Arabia", null, null, 0 },
                    { 127, "lby", null, null, null, "ly.png", "Libyan Arab Jamahiriya", null, null, 0 },
                    { 197, "srb", null, null, null, "rs.png", "Serbia", null, null, 0 },
                    { 198, "syc", null, null, null, "sc.png", "Seychelles", null, null, 0 },
                    { 199, "sle", null, null, null, "sl.png", "Sierra Leone", null, null, 0 },
                    { 200, "sgp", null, null, null, "sg.png", "Singapore", null, null, 0 },
                    { 201, "sxm", null, null, null, "sx.png", "Sint Maarten (Dutch Part)", null, null, 0 },
                    { 202, "svk", null, null, null, "sk.png", "Slovakia", null, null, 0 },
                    { 203, "svn", null, null, null, "si.png", "Slovenia", null, null, 0 },
                    { 189, "maf", null, null, null, "mf.png", "Saint Martin (French Part)", null, null, 0 },
                    { 204, "slb", null, null, null, "sb.png", "Solomon Islands", null, null, 0 },
                    { 206, "zaf", null, null, null, "za.png", "South Africa", null, null, 0 },
                    { 207, "sgt", null, null, null, "gs.png", "South Georgia and The South Sandwich Islands", null, null, 0 },
                    { 208, "ssd", null, null, null, "ss.png", "South Sudan", null, null, 0 },
                    { 209, "esp", null, null, null, "es.png", "Spain", null, null, 0 },
                    { 210, "lka", null, null, null, "lk.png", "Sri Lanka", null, null, 0 },
                    { 211, "sdn", null, null, null, "sd.png", "Sudan", null, null, 0 },
                    { 212, "sur", null, null, null, "sr.png", "Suriname", null, null, 0 },
                    { 213, "sjm", null, null, null, "sj.png", "Svalbard and Jan Mayen", null, null, 0 },
                    { 214, "swz", null, null, null, "sz.png", "Swaziland", null, null, 0 },
                    { 215, "swe", null, null, null, "se.png", "Sweden", null, null, 0 },
                    { 216, "che", null, null, null, "ch.png", "Switzerland", null, null, 0 },
                    { 217, "syr", null, null, null, "sy.png", "Syrian Arab Republic", null, null, 0 },
                    { 218, "tpc", null, null, null, "tw.png", "Taiwan, Province of China", null, null, 0 },
                    { 205, "som", null, null, null, "so.png", "Somalia", null, null, 0 },
                    { 126, "lbr", null, null, null, "lr.png", "Liberia", null, null, 0 },
                    { 118, "prk", null, null, null, "kp.png", "Korea, Democratic People's Republic of", null, null, 0 },
                    { 124, "lbn", null, null, null, "lb.png", "Lebanon", null, null, 0 },
                    { 33, "bio", null, null, null, "io.png", "British Indian Ocean Territory", null, null, 0 },
                    { 34, "brn", null, null, null, "bn.png", "Brunei Darussalam", null, null, 0 },
                    { 35, "bgr", null, null, null, "bg.png", "Bulgaria", null, null, 0 },
                    { 36, "bfa", null, null, null, "bf.png", "Burkina Faso", null, null, 0 },
                    { 37, "bdi", null, null, null, "bi.png", "Burundi", null, null, 0 },
                    { 38, "khm", null, null, null, "kh.png", "Cambodia", null, null, 0 },
                    { 39, "cmr", null, null, null, "cm.png", "Cameroon", null, null, 0 },
                    { 40, "can", null, null, null, "ca.png", "Canada", null, null, 0 },
                    { 41, "cpv", null, null, null, "cv.png", "Cape Verde", null, null, 0 },
                    { 42, "cym", null, null, null, "ky.png", "Cayman Islands", null, null, 0 },
                    { 43, "caf", null, null, null, "cf.png", "Central African Republic", null, null, 0 },
                    { 44, "tcd", null, null, null, "td.png", "Chad", null, null, 0 },
                    { 45, "chl", null, null, null, "cl.png", "Chile", null, null, 0 },
                    { 46, "chn", null, null, null, "cn.png", "China", null, null, 0 },
                    { 47, "chi", null, null, null, "cx.png", "Christmas Island", null, null, 0 },
                    { 48, "coc", null, null, null, "cc.png", "Cocos (Keeling) Islands", null, null, 0 },
                    { 49, "col", null, null, null, "co.png", "Colombia", null, null, 0 },
                    { 50, "com", null, null, null, "km.png", "Comoros", null, null, 0 },
                    { 51, "cog", null, null, null, "cg.png", "Congo", null, null, 0 },
                    { 52, "cod", null, null, null, "cd.png", "Congo, The Democratic Republic of the", null, null, 0 },
                    { 53, "cok", null, null, null, "ck.png", "Cook Islands", null, null, 0 },
                    { 54, "cri", null, null, null, "cr.png", "Costa Rica", null, null, 0 },
                    { 55, "civ", null, null, null, "ci.png", "Cote d'Ivoire", null, null, 0 },
                    { 56, "hrv", null, null, null, "hr.png", "Croatia", null, null, 0 },
                    { 57, "cub", null, null, null, "cu.png", "Cuba", null, null, 0 },
                    { 58, "cuw", null, null, null, "cw.png", "Curacao", null, null, 0 },
                    { 59, "cyp", null, null, null, "cy.png", "Cyprus", null, null, 0 },
                    { 32, "bra", null, null, null, "br.png", "Brazil", null, null, 0 },
                    { 31, "bvi", null, null, null, "bv.png", "Bouvet Island", null, null, 0 },
                    { 30, "bwa", null, null, null, "bw.png", "Botswana", null, null, 0 },
                    { 29, "bih", null, null, null, "ba.png", "Bosnia and Herzegovina", null, null, 0 },
                    { 1, "afg", null, null, null, "af.png", "Afghanistan", null, null, 0 },
                    { 2, "ala", null, null, null, "ax.png", "Aland Islands", null, null, 0 },
                    { 3, "alb", null, null, null, "al.png", "Albania", null, null, 0 },
                    { 4, "dza", null, null, null, "dz.png", "Algeria", null, null, 0 },
                    { 5, "asm", null, null, null, "as.png", "American Samoa", null, null, 0 },
                    { 6, "and", null, null, null, "ad.png", "Andorra", null, null, 0 },
                    { 7, "ago", null, null, null, "ao.png", "Angola", null, null, 0 },
                    { 8, "aia", null, null, null, "ai.png", "Anguilla", null, null, 0 },
                    { 9, "ant", null, null, null, "aq.png", "Antarctica", null, null, 0 },
                    { 10, "atg", null, null, null, "ag.png", "Antigua and Barbuda", null, null, 0 },
                    { 11, "arg", null, null, null, "ar.png", "Argentina", null, null, 0 },
                    { 12, "arm", null, null, null, "am.png", "Armenia", null, null, 0 },
                    { 13, "abw", null, null, null, "aw.png", "Aruba", null, null, 0 },
                    { 60, "cze", null, null, null, "cz.png", "Czech Republic", null, null, 0 },
                    { 14, "aus", null, null, null, "au.png", "Australia", null, null, 0 },
                    { 16, "aze", null, null, null, "az.png", "Azerbaijan", null, null, 0 },
                    { 17, "bhs", null, null, null, "bs.png", "Bahamas", null, null, 0 },
                    { 18, "bhr", null, null, null, "bh.png", "Bahrain", null, null, 0 },
                    { 19, "bgd", null, null, null, "bd.png", "Bangladesh", null, null, 0 },
                    { 20, "brb", null, null, null, "bb.png", "Barbados", null, null, 0 },
                    { 21, "blr", null, null, null, "by.png", "Belarus", null, null, 0 },
                    { 22, "bel", null, null, null, "be.png", "Belgium", null, null, 0 },
                    { 23, "blz", null, null, null, "bz.png", "Belize", null, null, 0 },
                    { 24, "ben", null, null, null, "bj.png", "Benin", null, null, 0 },
                    { 25, "bmu", null, null, null, "bm.png", "Bermuda", null, null, 0 },
                    { 26, "btn", null, null, null, "bt.png", "Bhutan", null, null, 0 },
                    { 27, "bol", null, null, null, "bo.png", "Bolivia, Plurinational State of", null, null, 0 },
                    { 28, "bes", null, null, null, "bq.png", "Bonaire, Sint Eustatius and Saba", null, null, 0 },
                    { 15, "aut", null, null, null, "at.png", "Austria", null, null, 0 },
                    { 61, "dnk", null, null, null, "dk.png", "Denmark", null, null, 0 },
                    { 62, "dji", null, null, null, "dj.png", "Djibouti", null, null, 0 },
                    { 63, "dma", null, null, null, "dm.png", "Dominica", null, null, 0 },
                    { 96, "hti", null, null, null, "ht.png", "Haiti", null, null, 0 },
                    { 97, "him", null, null, null, "hm.png", "Heard Island and McDonald Islands", null, null, 0 },
                    { 98, "vat", null, null, null, "va.png", "Holy See (Vatican City State)", null, null, 0 },
                    { 99, "hnd", null, null, null, "hn.png", "Honduras", null, null, 0 },
                    { 100, "hkg", null, null, null, "hk.png", "Hong Kong", null, null, 0 },
                    { 101, "hun", null, null, null, "hu.png", "Hungary", null, null, 0 },
                    { 102, "isl", null, null, null, "is.png", "Iceland", null, null, 0 },
                    { 103, "ind", null, null, null, "in.png", "India", null, null, 0 },
                    { 104, "idn", null, null, null, "id.png", "Indonesia", null, null, 0 },
                    { 105, "irn", null, null, null, "ir.png", "Iran, Islamic Republic of", null, null, 0 },
                    { 106, "irq", null, null, null, "iq.png", "Iraq", null, null, 0 },
                    { 107, "irl", null, null, null, "ie.png", "Ireland", null, null, 0 },
                    { 108, "imn", null, null, null, "im.png", "Isle of Man", null, null, 0 },
                    { 109, "isr", null, null, null, "il.png", "Israel", null, null, 0 },
                    { 110, "ita", null, null, null, "it.png", "Italy", null, null, 0 },
                    { 111, "jam", null, null, null, "jm.png", "Jamaica", null, null, 0 },
                    { 112, "jpn", null, null, null, "jp.png", "Japan", null, null, 0 },
                    { 113, "jey", null, null, null, "je.png", "Jersey", null, null, 0 },
                    { 114, "jor", null, null, null, "jo.png", "Jordan", null, null, 0 },
                    { 115, "kaz", null, null, null, "kz.png", "Kazakhstan", null, null, 0 },
                    { 116, "ken", null, null, null, "ke.png", "Kenya", null, null, 0 },
                    { 117, "kir", null, null, null, "ki.png", "Kiribati", null, null, 0 },
                    { 119, "kor", null, null, null, "kr.png", "Korea, Republic of", null, null, 0 },
                    { 120, "kwt", null, null, null, "kw.png", "Kuwait", null, null, 0 },
                    { 121, "kgz", null, null, null, "kg.png", "Kyrgyzstan", null, null, 0 },
                    { 122, "lao", null, null, null, "la.png", "Lao People's Democratic Republic", null, null, 0 },
                    { 123, "lva", null, null, null, "lv.png", "Latvia", null, null, 0 },
                    { 95, "guy", null, null, null, "gy.png", "Guyana", null, null, 0 }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "CreatedBy", "CreatedDate", "Description", "Flag", "Label", "ModifiedBy", "ModifiedDate", "Order" },
                values: new object[,]
                {
                    { 125, "lso", null, null, null, "ls.png", "Lesotho", null, null, 0 },
                    { 94, "gnb", null, null, null, "gw.png", "Guinea-Bissau", null, null, 0 },
                    { 92, "ggy", null, null, null, "gg.png", "Guernsey", null, null, 0 },
                    { 64, "dom", null, null, null, "do.png", "Dominican Republic", null, null, 0 },
                    { 65, "ecu", null, null, null, "ec.png", "Ecuador", null, null, 0 },
                    { 66, "egy", null, null, null, "eg.png", "Egypt", null, null, 0 },
                    { 67, "slv", null, null, null, "sv.png", "El Salvador", null, null, 0 },
                    { 68, "gnq", null, null, null, "gq.png", "Equatorial Guinea", null, null, 0 },
                    { 69, "eri", null, null, null, "er.png", "Eritrea", null, null, 0 },
                    { 70, "est", null, null, null, "ee.png", "Estonia", null, null, 0 },
                    { 71, "eth", null, null, null, "et.png", "Ethiopia", null, null, 0 },
                    { 72, "flk", null, null, null, "fk.png", "Falkland Islands (Malvinas)", null, null, 0 },
                    { 73, "fro", null, null, null, "fo.png", "Faroe Islands", null, null, 0 },
                    { 74, "fji", null, null, null, "fj.png", "Fiji", null, null, 0 },
                    { 75, "fin", null, null, null, "fi.png", "Finland", null, null, 0 },
                    { 76, "fra", null, null, null, "fr.png", "France", null, null, 0 },
                    { 93, "gin", null, null, null, "gn.png", "Guinea", null, null, 0 },
                    { 77, "guf", null, null, null, "gf.png", "French Guiana", null, null, 0 },
                    { 79, "fst", null, null, null, "tf.png", "French Southern Territories", null, null, 0 },
                    { 80, "gab", null, null, null, "ga.png", "Gabon", null, null, 0 },
                    { 81, "gmb", null, null, null, "gm.png", "Gambia", null, null, 0 },
                    { 82, "geo", null, null, null, "ge.png", "Georgia", null, null, 0 },
                    { 83, "deu", null, null, null, "de.png", "Germany", null, null, 0 },
                    { 84, "gha", null, null, null, "gh.png", "Ghana", null, null, 0 },
                    { 85, "gib", null, null, null, "gi.png", "Gibraltar", null, null, 0 },
                    { 86, "grc", null, null, null, "gr.png", "Greece", null, null, 0 },
                    { 87, "grl", null, null, null, "gl.png", "Greenland", null, null, 0 },
                    { 88, "grd", null, null, null, "gd.png", "Grenada", null, null, 0 },
                    { 89, "glp", null, null, null, "gp.png", "Guadeloupe", null, null, 0 },
                    { 90, "gum", null, null, null, "gu.png", "Guam", null, null, 0 },
                    { 91, "gtm", null, null, null, "gt.png", "Guatemala", null, null, 0 },
                    { 78, "pyf", null, null, null, "pf.png", "French Polynesia", null, null, 0 }
                });

            migrationBuilder.InsertData(
                table: "Seasons",
                columns: new[] { "Id", "Code", "CreatedBy", "CreatedDate", "Description", "EndDate", "Label", "ModifiedBy", "ModifiedDate", "Order", "StartDate" },
                values: new object[,]
                {
                    { 1, "17/18", null, null, null, new DateTime(2018, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "2017/2018", null, null, 1, new DateTime(2017, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "18/19", null, null, null, new DateTime(2019, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "2018/2019", null, null, 1, new DateTime(2018, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Adresses_CountryId",
                table: "Adresses",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_PersonId",
                table: "Contacts",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_PersonId1",
                table: "Contacts",
                column: "PersonId1");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_AddressId",
                table: "Persons",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_CountryId",
                table: "Persons",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_CategoryId",
                table: "Persons",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_RosterPlayers_PlayerId",
                table: "RosterPlayers",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Rosters_CategoryId",
                table: "Rosters",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "RosterPlayers");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "Rosters");

            migrationBuilder.DropTable(
                name: "Adresses");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Seasons");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
