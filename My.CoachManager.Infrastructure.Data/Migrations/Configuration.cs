using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using My.CoachManager.Domain.Entities;
using My.CoachManager.Infrastructure.Data.UnitOfWorks;
using My.CoachManager.CrossCutting.Core.Constants;
using My.CoachManager.CrossCutting.Core.Enums;
using My.CoachManager.CrossCutting.Core.Extensions;

namespace My.CoachManager.Infrastructure.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            SetSqlGenerator("System.Data.SqlClient", new DefaultValueSqlServerMigrationSqlGenerator());
        }

        protected override void Seed(DataContext context)
        {
            AddCategories(context);
            AddCountries(context);
            AddPositions(context);
            AddSeasons(context);

            AddTestData(context);
        }

        private void AddTestData(DataContext context)
        {
            var player1 = new Player()
            {
                CategoryId = 13,
                Address = new Address()
                {
                    Id = 1,
                    Row1 = "9 rue Marivaux",
                    PostalCode = "63000",
                    City = "Clermont-Ferrand"
                },
                AddressId = 1,
                Birthdate = new DateTime(1989, 12, 5),
                CountryId = 76,
                FirstName = "Stéphane",
                Gender = GenderType.Female,
                LastName = "André",
                Laterality = Laterality.LeftHander,
                PlaceOfBirth = "Nevers",
                ShoesSize = 44,
                Size = "L",
                Height = 175,
                Weight = 75
            };

            var player2 = new Player()
            {
                CategoryId = 3,
                Birthdate = new DateTime(1986, 12, 4),
                Address = new Address()
                {
                    Id = 2,
                    Row1 = "Impasse du Babory",
                    PostalCode = "63270",
                    City = "Vic le comte"
                },
                AddressId = 2,
                CountryId = 76,
                FirstName = "Vincent",
                Gender = GenderType.Male,
                LastName = "Sourdeix",
                Laterality = Laterality.RightHander,
                PlaceOfBirth = "Tulle",
                ShoesSize = 42,
                Size = "L",
                LicenseNumber = "123456789"
            };

            // Contacts
            var contacts = new List<Contact>
            {
                new Email
                {
                    Label = "Test",
                    Default = true,
                    Value = "andre.cs2i@gmail.com"
                },
                new Email
                {
                    Label = "Test2",
                    Default = false,
                    Value = "vincentsourdeix@gmail.com"
                },
                new Phone
                {
                    Label = "Test",
                    Default = true,
                    Value = "0664411391"
                }
            };

            var contacts2 = new List<Contact>
            {
                new Email
                {
                    Label = "Principale",
                    Default = true,
                    Value = "visourdeix@gmail.com"
                },
                new Email
                {
                    Label = "Pub",
                    Default = false,
                    Value = "vincentsourdeix@gmail.com"
                },
                new Phone
                {
                    Label = "Portable",
                    Default = true,
                    Value = "0679189256"
                }
            };

            player1.Contacts.AddRange(contacts);
            player2.Contacts.AddRange(contacts2);

            context.Players.AddOrUpdate(x => x.LastName, player1);
            context.Players.AddOrUpdate(x => x.LastName, player2);

            context.Commit();

            // Rosters
            var roster = new Roster()
            {
                Id = 1,
                Name = "U15 2017/2018",
                CategoryId = 7,
                SeasonId = 1
            };

            var squad1 = new Squad()
            {
                Id = 1,
                Name = "Equipe A"
            };

            var squad2 = new Squad()
            {
                Id = 2,
                Name = "Equipe B"
            };

            roster.Players.Add(new RosterPlayer()
            {
                LicenseState = LicenseState.Back,
                Number = 12,
                IsMutation = true,
                PlayerId = player1.Id,
                RosterId = 1,
                SquadId = 1,
            });
            roster.Players.Add(new RosterPlayer()
            {
                LicenseState = LicenseState.Given,
                Number = 9,
                PlayerId = player2.Id,
                RosterId = 1,
                SquadId = 2,
            });

            for (int i = 1; i <= 15; i++)
            {
                var player = new Player()
                {
                    Id = i + 10,
                    CategoryId = i,
                    Birthdate = new DateTime(1986, 12, i),
                    CountryId = 76,
                    FirstName = "FirstName" + i,
                    Gender = GenderType.Male,
                    LastName = "LastName" + i,
                    Laterality = Laterality.RightHander,
                    ShoesSize = 30 + i,
                    Size = "L",
                    LicenseNumber = "000000000"
                };
                context.Players.AddOrUpdate(x => x.LastName, player);

                roster.Players.Add(new RosterPlayer()
                {
                    LicenseState = LicenseState.Given,
                    Number = i,
                    PlayerId = player.Id,
                    RosterId = 1,
                    SquadId = 1,
                });
            }

            roster.Squads.Add(squad1);
            roster.Squads.Add(squad2);
            context.Rosters.AddOrUpdate(x => x.Id, roster);
            context.Commit();

            // User and permissions
            var perm1 = new Permission() { Id = 1, Code = PermissionConstants.ChangeUser, Label = "Changer d'utilisateur", Description = "Permet de se connecter à l'aplication en tant qu'un autre utilisateur." };
            var perm2 = new Permission() { Id = 2, Code = PermissionConstants.AccessAdmin, Label = "Accès à l'administration", Description = "Permet d'accèder à tout le module d'administration." };
            var role1 = new Role() { Id = 1, Code = RoleConstants.Admin, Label = "Administrateur", Description = "Rôle permettant de gérer toutes les données utilisées dan l''application." };

            var user1 = new User() { Id = 1, Name = "Stéphane ANDRE (Home)", Login = "andre", Password = "qRBfE9MoPFs=", Mail = "andre.cs2i@gmail.com", RosterId = 1 };
            var user2 = new User() { Id = 2, Name = "Stéphane ANDRE (Merial)", Login = "E0214719", Password = "qRBfE9MoPFs=", Mail = "stephane.andre@merial.com", RosterId = 1 };
            var user3 = new User() { Id = 3, Name = "Vincent SOURDEIX (BI)", Login = "E0268620", Password = "qRBfE9MoPFs=", Mail = "vincentsourdeix@test.fr", RosterId = 1 };

            role1.Permissions.Add(perm1);
            role1.Permissions.Add(perm2);
            user1.Roles.Add(role1);
            user2.Roles.Add(role1);
            user3.Roles.Add(role1);

            context.Permissions.AddOrUpdate(r => r.Label, perm1);
            context.Permissions.AddOrUpdate(r => r.Label, perm2);
            context.Commit();

            context.Roles.AddOrUpdate(r => r.Label, role1);
            context.Commit();

            context.Users.AddOrUpdate(u => u.Name, user1);
            context.Users.AddOrUpdate(u => u.Name, user2);
            context.Users.AddOrUpdate(u => u.Name, user3);
            context.Commit();
        }

        private void AddCategories(DataContext context)
        {
            context.Categories.AddOrUpdate(c => c.Label, new Category() { Label = "Séniors", Code = "S", Year = 1998, Order = 1 });
            context.Categories.AddOrUpdate(c => c.Label, new Category() { Label = "Vétérans", Code = "V", Year = 1982, Order = 2 });

            var year = 1999;
            var order = 3;
            for (var i = 19; i >= 6; i--)
            {
                var code = "U" + i;
                context.Categories.AddOrUpdate(c => c.Label, new Category() { Label = code, Code = code, Year = year, Order = order });
                year++;
                order++;
            }

            context.Commit();
        }

        private void AddPositions(DataContext context)
        {
            context.Positions.AddOrUpdate(s => s.Code, new Position() { Label = "Gardien", Code = "GB", Order = 1, Row = 1, Column = 3 });

            context.Positions.AddOrUpdate(s => s.Code, new Position() { Label = "Libéro", Code = "LB", Order = 2, Row = 2, Column = 3 });

            context.Positions.AddOrUpdate(s => s.Code, new Position() { Label = "Arrière gauche", Code = "DG", Order = 3, Row = 3, Column = 1 });
            context.Positions.AddOrUpdate(s => s.Code, new Position() { Label = "Défenseur central gauche", Code = "DCG", Order = 4, Row = 3, Column = 2 });
            context.Positions.AddOrUpdate(s => s.Code, new Position() { Label = "Stoppeur", Code = "S", Order = 5, Row = 3, Column = 3 });
            context.Positions.AddOrUpdate(s => s.Code, new Position() { Label = "Défenseur central droit", Code = "DCD", Order = 5, Row = 3, Column = 4 });
            context.Positions.AddOrUpdate(s => s.Code, new Position() { Label = "Arrière droit", Code = "DD", Order = 6, Row = 3, Column = 5 });

            context.Positions.AddOrUpdate(s => s.Code, new Position() { Label = "Latéral gauche", Code = "LG", Order = 7, Row = 4, Column = 1 });
            context.Positions.AddOrUpdate(s => s.Code, new Position() { Label = "Milieu défensif gauche", Code = "MDG", Order = 8, Row = 4, Column = 2 });
            context.Positions.AddOrUpdate(s => s.Code, new Position() { Label = "Milieu défensif", Code = "MDC", Order = 9, Row = 4, Column = 3 });
            context.Positions.AddOrUpdate(s => s.Code, new Position() { Label = "Milieu défensif droit", Code = "MDD", Order = 10, Row = 4, Column = 4 });
            context.Positions.AddOrUpdate(s => s.Code, new Position() { Label = "Latéral droit", Code = "LD", Order = 11, Row = 4, Column = 5 });

            context.Positions.AddOrUpdate(s => s.Code, new Position() { Label = "Milieu gauche", Code = "MG", Order = 12, Row = 5, Column = 1 });
            context.Positions.AddOrUpdate(s => s.Code, new Position() { Label = "Milieu central gauche", Code = "MCG", Order = 13, Row = 5, Column = 2 });
            context.Positions.AddOrUpdate(s => s.Code, new Position() { Label = "Milieu central", Code = "MC", Order = 14, Row = 5, Column = 3 });
            context.Positions.AddOrUpdate(s => s.Code, new Position() { Label = "Milieu central droit", Code = "MCD", Order = 15, Row = 5, Column = 4 });
            context.Positions.AddOrUpdate(s => s.Code, new Position() { Label = "Milieu droit", Code = "MD", Order = 16, Row = 5, Column = 5 });

            context.Positions.AddOrUpdate(s => s.Code, new Position() { Label = "Ailier gauche", Code = "AG", Order = 17, Row = 6, Column = 1 });
            context.Positions.AddOrUpdate(s => s.Code, new Position() { Label = "Milieu offensif gauche", Code = "MOG", Order = 18, Row = 6, Column = 2 });
            context.Positions.AddOrUpdate(s => s.Code, new Position() { Label = "Milieu offensif", Code = "MO", Order = 19, Row = 6, Column = 3 });
            context.Positions.AddOrUpdate(s => s.Code, new Position() { Label = "Milieu offensif droit", Code = "MOD", Order = 20, Row = 6, Column = 4 });
            context.Positions.AddOrUpdate(s => s.Code, new Position() { Label = "Ailier droit", Code = "AD", Order = 21, Row = 6, Column = 5 });

            context.Positions.AddOrUpdate(s => s.Code, new Position() { Label = "Attaquant", Code = "ATT", Order = 22, Row = 7, Column = 3 });
            context.Commit();
        }

        private void AddSeasons(DataContext context)
        {
            context.Seasons.AddOrUpdate(s => s.Label, new Season() { Label = "2017/2018", Code = "17/18", Order = 1, StartDate = new DateTime(2017, 08, 01), EndDate = new DateTime(2018, 07, 31) });
            context.Commit();
        }

        private void AddCountries(DataContext context)
        {
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Afghanistan", Code = "afg", Flag = "af.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Aland Islands", Code = "ala", Flag = "ax.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Albania", Code = "alb", Flag = "al.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Algeria", Code = "dza", Flag = "dz.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "American Samoa", Code = "asm", Flag = "as.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Andorra", Code = "and", Flag = "ad.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Angola", Code = "ago", Flag = "ao.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Anguilla", Code = "aia", Flag = "ai.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Antarctica", Code = "ant", Flag = "aq.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Antigua and Barbuda", Code = "atg", Flag = "ag.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Argentina", Code = "arg", Flag = "ar.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Armenia", Code = "arm", Flag = "am.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Aruba", Code = "abw", Flag = "aw.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Australia", Code = "aus", Flag = "au.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Austria", Code = "aut", Flag = "at.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Azerbaijan", Code = "aze", Flag = "az.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Bahamas", Code = "bhs", Flag = "bs.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Bahrain", Code = "bhr", Flag = "bh.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Bangladesh", Code = "bgd", Flag = "bd.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Barbados", Code = "brb", Flag = "bb.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Belarus", Code = "blr", Flag = "by.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Belgium", Code = "bel", Flag = "be.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Belize", Code = "blz", Flag = "bz.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Benin", Code = "ben", Flag = "bj.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Bermuda", Code = "bmu", Flag = "bm.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Bhutan", Code = "btn", Flag = "bt.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Bolivia, Plurinational State of", Code = "bol", Flag = "bo.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Bonaire, Sint Eustatius and Saba", Code = "bes", Flag = "bq.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Bosnia and Herzegovina", Code = "bih", Flag = "ba.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Botswana", Code = "bwa", Flag = "bw.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Bouvet Island", Code = "bvi", Flag = "bv.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Brazil", Code = "bra", Flag = "br.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "British Indian Ocean Territory", Code = "bio", Flag = "io.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Brunei Darussalam", Code = "brn", Flag = "bn.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Bulgaria", Code = "bgr", Flag = "bg.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Burkina Faso", Code = "bfa", Flag = "bf.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Burundi", Code = "bdi", Flag = "bi.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Cambodia", Code = "khm", Flag = "kh.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Cameroon", Code = "cmr", Flag = "cm.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Canada", Code = "can", Flag = "ca.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Cape Verde", Code = "cpv", Flag = "cv.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Cayman Islands", Code = "cym", Flag = "ky.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Central African Republic", Code = "caf", Flag = "cf.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Chad", Code = "tcd", Flag = "td.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Chile", Code = "chl", Flag = "cl.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "China", Code = "chn", Flag = "cn.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Christmas Island", Code = "chi", Flag = "cx.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Cocos (Keeling) Islands", Code = "coc", Flag = "cc.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Colombia", Code = "col", Flag = "co.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Comoros", Code = "com", Flag = "km.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Congo", Code = "cog", Flag = "cg.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Congo, The Democratic Republic of the", Code = "cod", Flag = "cd.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Cook Islands", Code = "cok", Flag = "ck.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Costa Rica", Code = "cri", Flag = "cr.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Cote d'Ivoire", Code = "civ", Flag = "ci.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Croatia", Code = "hrv", Flag = "hr.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Cuba", Code = "cub", Flag = "cu.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Curacao", Code = "cuw", Flag = "cw.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Cyprus", Code = "cyp", Flag = "cy.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Czech Republic", Code = "cze", Flag = "cz.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Denmark", Code = "dnk", Flag = "dk.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Djibouti", Code = "dji", Flag = "dj.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Dominica", Code = "dma", Flag = "dm.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Dominican Republic", Code = "dom", Flag = "do.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Ecuador", Code = "ecu", Flag = "ec.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Egypt", Code = "egy", Flag = "eg.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "El Salvador", Code = "slv", Flag = "sv.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Equatorial Guinea", Code = "gnq", Flag = "gq.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Eritrea", Code = "eri", Flag = "er.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Estonia", Code = "est", Flag = "ee.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Ethiopia", Code = "eth", Flag = "et.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Falkland Islands (Malvinas)", Code = "flk", Flag = "fk.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Faroe Islands", Code = "fro", Flag = "fo.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Fiji", Code = "fji", Flag = "fj.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Finland", Code = "fin", Flag = "fi.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "France", Code = "fra", Flag = "fr.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "French Guiana", Code = "guf", Flag = "gf.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "French Polynesia", Code = "pyf", Flag = "pf.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "French Southern Territories", Code = "fst", Flag = "tf.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Gabon", Code = "gab", Flag = "ga.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Gambia", Code = "gmb", Flag = "gm.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Georgia", Code = "geo", Flag = "ge.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Germany", Code = "deu", Flag = "de.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Ghana", Code = "gha", Flag = "gh.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Gibraltar", Code = "gib", Flag = "gi.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Greece", Code = "grc", Flag = "gr.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Greenland", Code = "grl", Flag = "gl.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Grenada", Code = "grd", Flag = "gd.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Guadeloupe", Code = "glp", Flag = "gp.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Guam", Code = "gum", Flag = "gu.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Guatemala", Code = "gtm", Flag = "gt.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Guernsey", Code = "ggy", Flag = "gg.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Guinea", Code = "gin", Flag = "gn.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Guinea-Bissau", Code = "gnb", Flag = "gw.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Guyana", Code = "guy", Flag = "gy.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Haiti", Code = "hti", Flag = "ht.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Heard Island and McDonald Islands", Code = "him", Flag = "hm.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Holy See (Vatican City State)", Code = "vat", Flag = "va.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Honduras", Code = "hnd", Flag = "hn.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Hong Kong", Code = "hkg", Flag = "hk.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Hungary", Code = "hun", Flag = "hu.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Iceland", Code = "isl", Flag = "is.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "India", Code = "ind", Flag = "in.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Indonesia", Code = "idn", Flag = "id.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Iran, Islamic Republic of", Code = "irn", Flag = "ir.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Iraq", Code = "irq", Flag = "iq.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Ireland", Code = "irl", Flag = "ie.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Isle of Man", Code = "imn", Flag = "im.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Israel", Code = "isr", Flag = "il.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Italy", Code = "ita", Flag = "it.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Jamaica", Code = "jam", Flag = "jm.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Japan", Code = "jpn", Flag = "jp.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Jersey", Code = "jey", Flag = "je.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Jordan", Code = "jor", Flag = "jo.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Kazakhstan", Code = "kaz", Flag = "kz.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Kenya", Code = "ken", Flag = "ke.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Kiribati", Code = "kir", Flag = "ki.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Korea, Democratic People's Republic of", Code = "prk", Flag = "kp.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Korea, Republic of", Code = "kor", Flag = "kr.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Kuwait", Code = "kwt", Flag = "kw.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Kyrgyzstan", Code = "kgz", Flag = "kg.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Lao People's Democratic Republic", Code = "lao", Flag = "la.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Latvia", Code = "lva", Flag = "lv.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Lebanon", Code = "lbn", Flag = "lb.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Lesotho", Code = "lso", Flag = "ls.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Liberia", Code = "lbr", Flag = "lr.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Libyan Arab Jamahiriya", Code = "lby", Flag = "ly.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Liechtenstein", Code = "lie", Flag = "li.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Lithuania", Code = "ltu", Flag = "lt.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Luxembourg", Code = "lux", Flag = "lu.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Macao", Code = "mac", Flag = "mo.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Macedonia, The former Yugoslav Republic of", Code = "mkd", Flag = "mk.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Madagascar", Code = "mdg", Flag = "mg.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Malawi", Code = "mwi", Flag = "mw.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Malaysia", Code = "mys", Flag = "my.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Maldives", Code = "mdv", Flag = "mv.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Mali", Code = "mli", Flag = "ml.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Malta", Code = "mlt", Flag = "mt.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Marshall Islands", Code = "mhl", Flag = "mh.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Martinique", Code = "mtq", Flag = "mq.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Mauritania", Code = "mrt", Flag = "mr.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Mauritius", Code = "mus", Flag = "mu.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Mayotte", Code = "myt", Flag = "yt.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Mexico", Code = "mex", Flag = "mx.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Micronesia, Federated States of", Code = "fsm", Flag = "fm.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Moldova, Republic of", Code = "mda", Flag = "md.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Monaco", Code = "mco", Flag = "mc.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Mongolia", Code = "mng", Flag = "mn.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Montenegro", Code = "mne", Flag = "me.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Montserrat", Code = "msr", Flag = "ms.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Morocco", Code = "mar", Flag = "ma.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Mozambique", Code = "moz", Flag = "mz.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Myanmar", Code = "mmr", Flag = "mm.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Namibia", Code = "nam", Flag = "na.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Nauru", Code = "nru", Flag = "nr.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Nepal", Code = "npl", Flag = "np.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Netherlands", Code = "nld", Flag = "nl.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "New Caledonia", Code = "ncl", Flag = "nc.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "New Zealand", Code = "nzl", Flag = "nz.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Nicaragua", Code = "nic", Flag = "ni.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Niger", Code = "ner", Flag = "ne.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Nigeria", Code = "nga", Flag = "ng.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Niue", Code = "niu", Flag = "nu.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Norfolk Island", Code = "nfk", Flag = "nf.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Northern Mariana Islands", Code = "mnp", Flag = "mp.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Norway", Code = "nor", Flag = "no.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Oman", Code = "omn", Flag = "om.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Pakistan", Code = "pak", Flag = "pk.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Palau", Code = "plw", Flag = "pw.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Palestinian Territory, Occupied", Code = "pse", Flag = "ps.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Panama", Code = "pan", Flag = "pa.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Papua New Guinea", Code = "png", Flag = "pg.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Paraguay", Code = "pry", Flag = "py.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Peru", Code = "per", Flag = "pe.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Philippines", Code = "phl", Flag = "ph.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Pitcairn", Code = "pcn", Flag = "pn.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Poland", Code = "pol", Flag = "pl.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Portugal", Code = "prt", Flag = "pt.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Puerto Rico", Code = "pri", Flag = "pr.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Qatar", Code = "qat", Flag = "qa.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Reunion", Code = "reu", Flag = "re.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Romania", Code = "rou", Flag = "ro.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Russian Federation", Code = "rus", Flag = "ru.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Rwanda", Code = "rwa", Flag = "rw.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Saint Barthelemy", Code = "blm", Flag = "bl.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Saint Helena, Ascension and Tristan Da Cunha", Code = "shn", Flag = "sh.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Saint Kitts and Nevis", Code = "kna", Flag = "kn.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Saint Lucia", Code = "lca", Flag = "lc.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Saint Martin (French Part)", Code = "maf", Flag = "mf.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Saint Pierre and Miquelon", Code = "spm", Flag = "pm.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Saint Vincent and The Grenadines", Code = "vct", Flag = "vc.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Samoa", Code = "wsm", Flag = "ws.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "San Marino", Code = "smr", Flag = "sm.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Sao Tome and Principe", Code = "stp", Flag = "st.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Saudi Arabia", Code = "sau", Flag = "sa.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Senegal", Code = "sen", Flag = "sn.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Serbia", Code = "srb", Flag = "rs.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Seychelles", Code = "syc", Flag = "sc.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Sierra Leone", Code = "sle", Flag = "sl.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Singapore", Code = "sgp", Flag = "sg.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Sint Maarten (Dutch Part)", Code = "sxm", Flag = "sx.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Slovakia", Code = "svk", Flag = "sk.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Slovenia", Code = "svn", Flag = "si.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Solomon Islands", Code = "slb", Flag = "sb.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Somalia", Code = "som", Flag = "so.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "South Africa", Code = "zaf", Flag = "za.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "South Georgia and The South Sandwich Islands", Code = "sgt", Flag = "gs.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "South Sudan", Code = "ssd", Flag = "ss.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Spain", Code = "esp", Flag = "es.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Sri Lanka", Code = "lka", Flag = "lk.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Sudan", Code = "sdn", Flag = "sd.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Suriname", Code = "sur", Flag = "sr.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Svalbard and Jan Mayen", Code = "sjm", Flag = "sj.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Swaziland", Code = "swz", Flag = "sz.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Sweden", Code = "swe", Flag = "se.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Switzerland", Code = "che", Flag = "ch.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Syrian Arab Republic", Code = "syr", Flag = "sy.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Taiwan, Province of China", Code = "tpc", Flag = "tw.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Tajikistan", Code = "tjk", Flag = "tj.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Tanzania, United Republic of", Code = "tza", Flag = "tz.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Thailand", Code = "tha", Flag = "th.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Timor-Leste", Code = "tls", Flag = "tl.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Togo", Code = "tgo", Flag = "tg.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Tokelau", Code = "tkl", Flag = "tk.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Tonga", Code = "ton", Flag = "to.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Trinidad and Tobago", Code = "tto", Flag = "tt.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Tunisia", Code = "tun", Flag = "tn.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Turkey", Code = "tur", Flag = "tr.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Turkmenistan", Code = "tkm", Flag = "tm.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Turks and Caicos Islands", Code = "tca", Flag = "tc.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Tuvalu", Code = "tuv", Flag = "tv.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Uganda", Code = "uga", Flag = "ug.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Ukraine", Code = "ukr", Flag = "ua.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "United Arab Emirates", Code = "are", Flag = "ae.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "United Kingdom", Code = "gbr", Flag = "gb.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "United States", Code = "usa", Flag = "us.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "United States Minor Outlying Islands", Code = "usm", Flag = "um.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Uruguay", Code = "ury", Flag = "uy.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Uzbekistan", Code = "uzb", Flag = "uz.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Vanuatu", Code = "vut", Flag = "vu.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Venezuela, Bolivarian Republic of", Code = "ven", Flag = "ve.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Viet Nam", Code = "vnm", Flag = "vn.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Virgin Islands, British", Code = "vgb", Flag = "vg.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Virgin Islands, U.S.", Code = "vir", Flag = "vi.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Wallis and Futuna", Code = "wlf", Flag = "wf.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Western Sahara", Code = "esh", Flag = "eh.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Yemen", Code = "yem", Flag = "ye.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Zambia", Code = "zmb", Flag = "zm.png" });
            context.Countries.AddOrUpdate(c => c.Label, new Country() { Label = "Zimbabwe", Code = "zwe", Flag = "zw.png" });

            context.Commit();
        }
    }
}