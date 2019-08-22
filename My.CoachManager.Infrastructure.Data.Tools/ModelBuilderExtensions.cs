using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using My.CoachManager.CrossCutting.Core.Constants;
using My.CoachManager.CrossCutting.Core.Enums;
using My.CoachManager.CrossCutting.Core.Extensions;
using My.CoachManager.CrossCutting.Core.Generators;
using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Infrastructure.Data.Tools
{
    public static class ModelBuilderExtensions
    {
        private const int NbPlayers = 500;
        private const int NbRosters = 7;
        private const string CreatedUser = "ModelBuilder";

        /// <summary>
        /// Seeds data.
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var seasons = GetSeasons().ToArray();
            var categories = GetCategories().ToArray();
            var players = GetPlayers(NbPlayers).ToArray();
            var addresses = GetAddresses(NbPlayers).ToArray();
            var contacts = GetContacts(players).ToArray();
            var playerPositions = GetPlayerPositions(players).ToArray();
            var rosters = GetRosters(categories, NbRosters).ToArray();
            var squads = GetSquads(rosters).ToArray();
            var rosterPlayers = GetRosterPlayers(players, rosters, squads, seasons, categories).ToArray();
            var trainings = GetTrainings(rosters, seasons).ToArray();
            var trainingAttendances = GetTrainingsAttendances(trainings, rosterPlayers).ToArray();
            var injuries = GetInjuries(players).ToArray();

            modelBuilder.Entity<Category>().HasData(categories);
            modelBuilder.Entity<Position>().HasData(GetPositions().ToArray());
            modelBuilder.Entity<Season>().HasData(seasons);
            modelBuilder.Entity<Country>().HasData(GetCountries().ToArray());
            modelBuilder.Entity<Address>().HasData(addresses);
            modelBuilder.Entity<Player>().HasData(players);
            modelBuilder.Entity<Email>().HasData(contacts.OfType<Email>().ToArray());
            modelBuilder.Entity<Phone>().HasData(contacts.OfType<Phone>().ToArray());
            modelBuilder.Entity<PlayerPosition>().HasData(playerPositions);
            modelBuilder.Entity<Roster>().HasData(rosters);
            modelBuilder.Entity<Squad>().HasData(squads);
            modelBuilder.Entity<RosterPlayer>().HasData(rosterPlayers);
            modelBuilder.Entity<Training>().HasData(trainings);
            modelBuilder.Entity<TrainingAttendance>().HasData(trainingAttendances);
            modelBuilder.Entity<Injury>().HasData(injuries);
            modelBuilder.Entity<User>().HasData(GetUsers().ToArray());
            
        }

        /// <summary>
        /// Seeds categories.
        /// </summary>
        private static IList<Category> GetCategories()
        {
            var result = new List<Category>
            {
                new Category() {Id = 1, Label = "Séniors", Code = "S", Age = 19, Order = 1, CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
                new Category() {Id = 2, Label = "Vétérans", Code = "V", Age = 35, Order = 2, CreatedBy = CreatedUser, CreatedDate = DateTime.Now}
            };

            var order = 3;
            for (var i = 19; i >= 6; i--)
            {
                var code = "U" + i;
                result.Add(new Category() { Id = order, Label = code, Code = code, Age = i - 1, Order = order, CreatedBy = CreatedUser, CreatedDate = DateTime.Now });
                order++;
            }

            return result;
        }

        /// <summary>
        /// Seeds seasons.
        /// </summary>
        private static IList<Season> GetSeasons()
        {
            var result = new List<Season>
            {
                new Season {Id = 1, Label = "2017/2018", Code = "17/18", Order = 3, StartDate = new DateTime(2017, 08, 01), EndDate = new DateTime(2018, 07, 31), CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
                new Season {Id = 2, Label = "2018/2019", Code = "18/19", Order = 2, StartDate = new DateTime(2018, 08, 01), EndDate = new DateTime(2019, 07, 31), CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
                new Season {Id = 3, Label = "2019/2020", Code = "19/20", Order = 1, StartDate = new DateTime(2019, 08, 01), EndDate = new DateTime(2020, 07, 31), CreatedBy = CreatedUser, CreatedDate = DateTime.Now}
            };

            return result;
        }

        /// <summary>
        /// Seeds seasons.
        /// </summary>
        private static IList<Position> GetPositions()
        {
            var result = new List<Position>
            {
                new Position() {Id = 1, Label = "Gardien", Code = "GB", Order = 1, Type =PositionType.GoalKeeper, Side = PositionSide.Center, Row = 6, Column = 1, CreatedBy = CreatedUser, CreatedDate = DateTime.Now},

                new Position() {Id = 2, Label = "Libéro", Code = "L", Order = 2, Type =PositionType.Sweeper, Side = PositionSide.Center, Row = 5, Column = 1, CreatedBy = CreatedUser, CreatedDate = DateTime.Now},

                new Position() {Id = 3, Label = "Défenseur gauche", Code = "DG", Order = 3, Type =PositionType.FullBack, Side = PositionSide.Left, Row = 4, Column = 0, CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
                new Position() {Id = 4, Label = "Défenseur central", Code = "DC", Order = 4, Type =PositionType.CenterBack, Side = PositionSide.Center, Row = 4, Column = 1, CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
                new Position() {Id = 5, Label = "Défenseur Droit", Code = "DD", Order = 5, Type =PositionType.FullBack, Side = PositionSide.Right, Row = 4, Column = 2, CreatedBy = CreatedUser, CreatedDate = DateTime.Now},

                new Position() {Id = 6, Label = "Latéral Gauche", Code = "LG", Order = 6, Type =PositionType.WingBack, Side = PositionSide.Left, Row = 3, Column = 0, CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
                new Position() {Id = 7, Label = "Latéral Droit", Code = "LD", Order = 7, Type =PositionType.WingBack, Side = PositionSide.Left, Row = 3, Column = 2, CreatedBy = CreatedUser, CreatedDate = DateTime.Now},

                new Position() {Id = 8, Label = "Milieu défensif", Code = "MDC", Order = 8, Type =PositionType.DefensiveMidfielder, Side = PositionSide.Center, Row = 3, Column = 1, CreatedBy = CreatedUser, CreatedDate = DateTime.Now},

                new Position() {Id = 9, Label = "Milieu gauche", Code = "MG", Order = 9, Type =PositionType.Midfielder, Side = PositionSide.Left, Row = 2, Column = 0, CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
                new Position() {Id = 10, Label = "Milieu central", Code = "MC", Order = 10, Type =PositionType.Midfielder, Side = PositionSide.Center, Row = 2, Column = 1, CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
                new Position() {Id = 11, Label = "Milieu Droit", Code = "MD", Order = 11, Type =PositionType.Midfielder, Side = PositionSide.Right, Row = 2, Column = 2, CreatedBy = CreatedUser, CreatedDate = DateTime.Now},

                new Position() {Id = 12, Label = "Milieu offensif gauche", Code = "MOG", Order = 12, Type =PositionType.AttackingMidfielder, Side = PositionSide.Left, Row = 1, Column = 0, CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
                new Position() {Id = 13, Label = "Milieu offensif central", Code = "MOC", Order = 13, Type =PositionType.AttackingMidfielder, Side = PositionSide.Center, Row = 1, Column = 1, CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
                new Position() {Id = 14, Label = "Milieu offensif Droit", Code = "MOD", Order = 14, Type =PositionType.AttackingMidfielder, Side = PositionSide.Right, Row = 1, Column = 2, CreatedBy = CreatedUser, CreatedDate = DateTime.Now},

                new Position() {Id = 15, Label = "Ailier gauche", Code = "AG", Order = 15, Type =PositionType.Winger, Side = PositionSide.Left, Row = 0, Column = 0, CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
                new Position() {Id = 16, Label = "Attaquant", Code = "ATT", Order = 16, Type =PositionType.Forward, Side = PositionSide.Center, Row = 0, Column = 1, CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
                new Position() {Id = 17, Label = "Ailier Droit", Code = "AD", Order = 17, Type =PositionType.Winger, Side = PositionSide.Right, Row = 0, Column = 2, CreatedBy = CreatedUser, CreatedDate = DateTime.Now}
            };

            return result;
        }

        /// <summary>
        /// Seeds countries.
        /// </summary>
        private static IList<Country> GetCountries()
        {
            var result = new List<Country>
            {
            new Country() {Id = 1, Label = "Afghanistan", Code = "afg", Flag = "af.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 2, Label = "Aland Islands", Code = "ala", Flag = "ax.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 3, Label = "Albania", Code = "alb", Flag = "al.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 4, Label = "Algeria", Code = "dza", Flag = "dz.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 5, Label = "American Samoa", Code = "asm", Flag = "as.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 6, Label = "Andorra", Code = "and", Flag = "ad.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 7, Label = "Angola", Code = "ago", Flag = "ao.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 8, Label = "Anguilla", Code = "aia", Flag = "ai.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 9, Label = "Antarctica", Code = "ant", Flag = "aq.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 10, Label = "Antigua and Barbuda", Code = "atg", Flag = "ag.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 11, Label = "Argentina", Code = "arg", Flag = "ar.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 12, Label = "Armenia", Code = "arm", Flag = "am.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 13, Label = "Aruba", Code = "abw", Flag = "aw.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 14, Label = "Australia", Code = "aus", Flag = "au.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 15, Label = "Austria", Code = "aut", Flag = "at.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 16, Label = "Azerbaijan", Code = "aze", Flag = "az.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 17, Label = "Bahamas", Code = "bhs", Flag = "bs.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 18, Label = "Bahrain", Code = "bhr", Flag = "bh.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 19, Label = "Bangladesh", Code = "bgd", Flag = "bd.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 20, Label = "Barbados", Code = "brb", Flag = "bb.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 21, Label = "Belarus", Code = "blr", Flag = "by.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 22, Label = "Belgium", Code = "bel", Flag = "be.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 23, Label = "Belize", Code = "blz", Flag = "bz.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 24, Label = "Benin", Code = "ben", Flag = "bj.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 25, Label = "Bermuda", Code = "bmu", Flag = "bm.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 26, Label = "Bhutan", Code = "btn", Flag = "bt.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 27, Label = "Bolivia, Plurinational State of", Code = "bol", Flag = "bo.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 28, Label = "Bonaire, Sint Eustatius and Saba", Code = "bes", Flag = "bq.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 29, Label = "Bosnia and Herzegovina", Code = "bih", Flag = "ba.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 30, Label = "Botswana", Code = "bwa", Flag = "bw.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 31, Label = "Bouvet Island", Code = "bvi", Flag = "bv.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 32, Label = "Brazil", Code = "bra", Flag = "br.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 33, Label = "British Indian Ocean Territory", Code = "bio", Flag = "io.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 34, Label = "Brunei Darussalam", Code = "brn", Flag = "bn.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 35, Label = "Bulgaria", Code = "bgr", Flag = "bg.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 36, Label = "Burkina Faso", Code = "bfa", Flag = "bf.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 37, Label = "Burundi", Code = "bdi", Flag = "bi.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 38, Label = "Cambodia", Code = "khm", Flag = "kh.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 39, Label = "Cameroon", Code = "cmr", Flag = "cm.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 40, Label = "Canada", Code = "can", Flag = "ca.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 41, Label = "Cape Verde", Code = "cpv", Flag = "cv.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 42, Label = "Cayman Islands", Code = "cym", Flag = "ky.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 43, Label = "Central African Republic", Code = "caf", Flag = "cf.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 44, Label = "Chad", Code = "tcd", Flag = "td.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 45, Label = "Chile", Code = "chl", Flag = "cl.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 46, Label = "China", Code = "chn", Flag = "cn.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 47, Label = "Christmas Island", Code = "chi", Flag = "cx.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 48, Label = "Cocos (Keeling) Islands", Code = "coc", Flag = "cc.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 49, Label = "Colombia", Code = "col", Flag = "co.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 50, Label = "Comoros", Code = "com", Flag = "km.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 51, Label = "Congo", Code = "cog", Flag = "cg.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 52, Label = "Congo, The Democratic Republic of the", Code = "cod", Flag = "cd.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 53, Label = "Cook Islands", Code = "cok", Flag = "ck.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 54, Label = "Costa Rica", Code = "cri", Flag = "cr.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 55, Label = "Cote d'Ivoire", Code = "civ", Flag = "ci.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 56, Label = "Croatia", Code = "hrv", Flag = "hr.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 57, Label = "Cuba", Code = "cub", Flag = "cu.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 58, Label = "Curacao", Code = "cuw", Flag = "cw.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 59, Label = "Cyprus", Code = "cyp", Flag = "cy.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 60, Label = "Czech Republic", Code = "cze", Flag = "cz.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 61, Label = "Denmark", Code = "dnk", Flag = "dk.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 62, Label = "Djibouti", Code = "dji", Flag = "dj.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 63, Label = "Dominica", Code = "dma", Flag = "dm.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 64, Label = "Dominican Republic", Code = "dom", Flag = "do.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 65, Label = "Ecuador", Code = "ecu", Flag = "ec.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 66, Label = "Egypt", Code = "egy", Flag = "eg.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 67, Label = "El Salvador", Code = "slv", Flag = "sv.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 68, Label = "Equatorial Guinea", Code = "gnq", Flag = "gq.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 69, Label = "Eritrea", Code = "eri", Flag = "er.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 70, Label = "Estonia", Code = "est", Flag = "ee.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 71, Label = "Ethiopia", Code = "eth", Flag = "et.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 72, Label = "Falkland Islands (Malvinas)", Code = "flk", Flag = "fk.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 73, Label = "Faroe Islands", Code = "fro", Flag = "fo.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 74, Label = "Fiji", Code = "fji", Flag = "fj.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 75, Label = "Finland", Code = "fin", Flag = "fi.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 76, Label = "France", Code = "fra", Flag = "fr.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 77, Label = "French Guiana", Code = "guf", Flag = "gf.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 78, Label = "French Polynesia", Code = "pyf", Flag = "pf.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 79, Label = "French Southern Territories", Code = "fst", Flag = "tf.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 80, Label = "Gabon", Code = "gab", Flag = "ga.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 81, Label = "Gambia", Code = "gmb", Flag = "gm.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 82, Label = "Georgia", Code = "geo", Flag = "ge.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 83, Label = "Germany", Code = "deu", Flag = "de.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 84, Label = "Ghana", Code = "gha", Flag = "gh.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 85, Label = "Gibraltar", Code = "gib", Flag = "gi.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 86, Label = "Greece", Code = "grc", Flag = "gr.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 87, Label = "Greenland", Code = "grl", Flag = "gl.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 88, Label = "Grenada", Code = "grd", Flag = "gd.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 89, Label = "Guadeloupe", Code = "glp", Flag = "gp.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 90, Label = "Guam", Code = "gum", Flag = "gu.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 91, Label = "Guatemala", Code = "gtm", Flag = "gt.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 92, Label = "Guernsey", Code = "ggy", Flag = "gg.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 93, Label = "Guinea", Code = "gin", Flag = "gn.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 94, Label = "Guinea-Bissau", Code = "gnb", Flag = "gw.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 95, Label = "Guyana", Code = "guy", Flag = "gy.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 96, Label = "Haiti", Code = "hti", Flag = "ht.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 97, Label = "Heard Island and McDonald Islands", Code = "him", Flag = "hm.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 98, Label = "Holy See (Vatican City State)", Code = "vat", Flag = "va.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 99, Label = "Honduras", Code = "hnd", Flag = "hn.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 100, Label = "Hong Kong", Code = "hkg", Flag = "hk.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 101, Label = "Hungary", Code = "hun", Flag = "hu.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 102, Label = "Iceland", Code = "isl", Flag = "is.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 103, Label = "India", Code = "ind", Flag = "in.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 104, Label = "Indonesia", Code = "idn", Flag = "id.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 105, Label = "Iran, Islamic Republic of", Code = "irn", Flag = "ir.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 106, Label = "Iraq", Code = "irq", Flag = "iq.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 107, Label = "Ireland", Code = "irl", Flag = "ie.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 108, Label = "Isle of Man", Code = "imn", Flag = "im.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 109, Label = "Israel", Code = "isr", Flag = "il.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 110, Label = "Italy", Code = "ita", Flag = "it.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 111, Label = "Jamaica", Code = "jam", Flag = "jm.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 112, Label = "Japan", Code = "jpn", Flag = "jp.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 113, Label = "Jersey", Code = "jey", Flag = "je.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 114, Label = "Jordan", Code = "jor", Flag = "jo.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 115, Label = "Kazakhstan", Code = "kaz", Flag = "kz.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 116, Label = "Kenya", Code = "ken", Flag = "ke.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 117, Label = "Kiribati", Code = "kir", Flag = "ki.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 118, Label = "Korea, Democratic People's Republic of", Code = "prk", Flag = "kp.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 119, Label = "Korea, Republic of", Code = "kor", Flag = "kr.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 120, Label = "Kuwait", Code = "kwt", Flag = "kw.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 121, Label = "Kyrgyzstan", Code = "kgz", Flag = "kg.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 122, Label = "Lao People's Democratic Republic", Code = "lao", Flag = "la.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 123, Label = "Latvia", Code = "lva", Flag = "lv.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 124, Label = "Lebanon", Code = "lbn", Flag = "lb.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 125, Label = "Lesotho", Code = "lso", Flag = "ls.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 126, Label = "Liberia", Code = "lbr", Flag = "lr.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 127, Label = "Libyan Arab Jamahiriya", Code = "lby", Flag = "ly.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 128, Label = "Liechtenstein", Code = "lie", Flag = "li.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 129, Label = "Lithuania", Code = "ltu", Flag = "lt.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 130, Label = "Luxembourg", Code = "lux", Flag = "lu.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 131, Label = "Macao", Code = "mac", Flag = "mo.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 132, Label = "Macedonia, The former Yugoslav Republic of", Code = "mkd", Flag = "mk.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 133, Label = "Madagascar", Code = "mdg", Flag = "mg.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 134, Label = "Malawi", Code = "mwi", Flag = "mw.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 135, Label = "Malaysia", Code = "mys", Flag = "my.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 136, Label = "Maldives", Code = "mdv", Flag = "mv.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 137, Label = "Mali", Code = "mli", Flag = "ml.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 138, Label = "Malta", Code = "mlt", Flag = "mt.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 139, Label = "Marshall Islands", Code = "mhl", Flag = "mh.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 140, Label = "Martinique", Code = "mtq", Flag = "mq.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 141, Label = "Mauritania", Code = "mrt", Flag = "mr.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 142, Label = "Mauritius", Code = "mus", Flag = "mu.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 143, Label = "Mayotte", Code = "myt", Flag = "yt.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 144, Label = "Mexico", Code = "mex", Flag = "mx.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 145, Label = "Micronesia, Federated States of", Code = "fsm", Flag = "fm.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 146, Label = "Moldova, Republic of", Code = "mda", Flag = "md.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 147, Label = "Monaco", Code = "mco", Flag = "mc.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 148, Label = "Mongolia", Code = "mng", Flag = "mn.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 149, Label = "Montenegro", Code = "mne", Flag = "me.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 150, Label = "Montserrat", Code = "msr", Flag = "ms.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 151, Label = "Morocco", Code = "mar", Flag = "ma.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 152, Label = "Mozambique", Code = "moz", Flag = "mz.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 153, Label = "Myanmar", Code = "mmr", Flag = "mm.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 154, Label = "Namibia", Code = "nam", Flag = "na.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 155, Label = "Nauru", Code = "nru", Flag = "nr.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 156, Label = "Nepal", Code = "npl", Flag = "np.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 157, Label = "Netherlands", Code = "nld", Flag = "nl.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 158, Label = "New Caledonia", Code = "ncl", Flag = "nc.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 159, Label = "New Zealand", Code = "nzl", Flag = "nz.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 160, Label = "Nicaragua", Code = "nic", Flag = "ni.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 161, Label = "Niger", Code = "ner", Flag = "ne.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 162, Label = "Nigeria", Code = "nga", Flag = "ng.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 163, Label = "Niue", Code = "niu", Flag = "nu.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 164, Label = "Norfolk Island", Code = "nfk", Flag = "nf.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 165, Label = "Northern Mariana Islands", Code = "mnp", Flag = "mp.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 166, Label = "Norway", Code = "nor", Flag = "no.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 167, Label = "Oman", Code = "omn", Flag = "om.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 168, Label = "Pakistan", Code = "pak", Flag = "pk.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 169, Label = "Palau", Code = "plw", Flag = "pw.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 170, Label = "Palestinian Territory, Occupied", Code = "pse", Flag = "ps.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 171, Label = "Panama", Code = "pan", Flag = "pa.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 172, Label = "Papua New Guinea", Code = "png", Flag = "pg.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 173, Label = "Paraguay", Code = "pry", Flag = "py.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 174, Label = "Peru", Code = "per", Flag = "pe.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 175, Label = "Philippines", Code = "phl", Flag = "ph.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 176, Label = "Pitcairn", Code = "pcn", Flag = "pn.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 177, Label = "Poland", Code = "pol", Flag = "pl.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 178, Label = "Portugal", Code = "prt", Flag = "pt.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 179, Label = "Puerto Rico", Code = "pri", Flag = "pr.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 180, Label = "Qatar", Code = "qat", Flag = "qa.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 181, Label = "Reunion", Code = "reu", Flag = "re.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 182, Label = "Romania", Code = "rou", Flag = "ro.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 183, Label = "Russian Federation", Code = "rus", Flag = "ru.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 184, Label = "Rwanda", Code = "rwa", Flag = "rw.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 185, Label = "Saint Barthelemy", Code = "blm", Flag = "bl.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 186, Label = "Saint Helena, Ascension and Tristan Da Cunha", Code = "shn", Flag = "sh.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 187, Label = "Saint Kitts and Nevis", Code = "kna", Flag = "kn.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 188, Label = "Saint Lucia", Code = "lca", Flag = "lc.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 189, Label = "Saint Martin (French Part)", Code = "maf", Flag = "mf.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 190, Label = "Saint Pierre and Miquelon", Code = "spm", Flag = "pm.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 191, Label = "Saint Vincent and The Grenadines", Code = "vct", Flag = "vc.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 192, Label = "Samoa", Code = "wsm", Flag = "ws.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 193, Label = "San Marino", Code = "smr", Flag = "sm.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 194, Label = "Sao Tome and Principe", Code = "stp", Flag = "st.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 195, Label = "Saudi Arabia", Code = "sau", Flag = "sa.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 196, Label = "Senegal", Code = "sen", Flag = "sn.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 197, Label = "Serbia", Code = "srb", Flag = "rs.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 198, Label = "Seychelles", Code = "syc", Flag = "sc.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 199, Label = "Sierra Leone", Code = "sle", Flag = "sl.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 200, Label = "Singapore", Code = "sgp", Flag = "sg.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 201, Label = "Sint Maarten (Dutch Part)", Code = "sxm", Flag = "sx.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 202, Label = "Slovakia", Code = "svk", Flag = "sk.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 203, Label = "Slovenia", Code = "svn", Flag = "si.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 204, Label = "Solomon Islands", Code = "slb", Flag = "sb.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 205, Label = "Somalia", Code = "som", Flag = "so.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 206, Label = "South Africa", Code = "zaf", Flag = "za.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 207, Label = "South Georgia and The South Sandwich Islands", Code = "sgt", Flag = "gs.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 208, Label = "South Sudan", Code = "ssd", Flag = "ss.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 209, Label = "Spain", Code = "esp", Flag = "es.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 210, Label = "Sri Lanka", Code = "lka", Flag = "lk.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 211, Label = "Sudan", Code = "sdn", Flag = "sd.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 212, Label = "Suriname", Code = "sur", Flag = "sr.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 213, Label = "Svalbard and Jan Mayen", Code = "sjm", Flag = "sj.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 214, Label = "Swaziland", Code = "swz", Flag = "sz.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 215, Label = "Sweden", Code = "swe", Flag = "se.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 216, Label = "Switzerland", Code = "che", Flag = "ch.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 217, Label = "Syrian Arab Republic", Code = "syr", Flag = "sy.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 218, Label = "Taiwan, Province of China", Code = "tpc", Flag = "tw.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 219, Label = "Tajikistan", Code = "tjk", Flag = "tj.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 220, Label = "Tanzania, United Republic of", Code = "tza", Flag = "tz.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 221, Label = "Thailand", Code = "tha", Flag = "th.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 222, Label = "Timor-Leste", Code = "tls", Flag = "tl.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 223, Label = "Togo", Code = "tgo", Flag = "tg.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 224, Label = "Tokelau", Code = "tkl", Flag = "tk.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 225, Label = "Tonga", Code = "ton", Flag = "to.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 226, Label = "Trinidad and Tobago", Code = "tto", Flag = "tt.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 227, Label = "Tunisia", Code = "tun", Flag = "tn.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 228, Label = "Turkey", Code = "tur", Flag = "tr.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 229, Label = "Turkmenistan", Code = "tkm", Flag = "tm.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 230, Label = "Turks and Caicos Islands", Code = "tca", Flag = "tc.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 231, Label = "Tuvalu", Code = "tuv", Flag = "tv.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 232, Label = "Uganda", Code = "uga", Flag = "ug.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 233, Label = "Ukraine", Code = "ukr", Flag = "ua.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 234, Label = "United Arab Emirates", Code = "are", Flag = "ae.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 235, Label = "United Kingdom", Code = "gbr", Flag = "gb.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 236, Label = "United States", Code = "usa", Flag = "us.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 237, Label = "United States Minor Outlying Islands", Code = "usm", Flag = "um.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 238, Label = "Uruguay", Code = "ury", Flag = "uy.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 239, Label = "Uzbekistan", Code = "uzb", Flag = "uz.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 240, Label = "Vanuatu", Code = "vut", Flag = "vu.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 241, Label = "Venezuela, Bolivarian Republic of", Code = "ven", Flag = "ve.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 242, Label = "Viet Nam", Code = "vnm", Flag = "vn.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 243, Label = "Virgin Islands, British", Code = "vgb", Flag = "vg.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 244, Label = "Virgin Islands, U.S.", Code = "vir", Flag = "vi.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 245, Label = "Wallis and Futuna", Code = "wlf", Flag = "wf.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 246, Label = "Western Sahara", Code = "esh", Flag = "eh.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 247, Label = "Yemen", Code = "yem", Flag = "ye.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 248, Label = "Zambia", Code = "zmb", Flag = "zm.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new Country() {Id = 249, Label = "Zimbabwe", Code = "zwe", Flag = "zw.png", CreatedBy = CreatedUser, CreatedDate = DateTime.Now}
            };

            return result;
        }

        /// <summary>
        /// Seeds players.
        /// </summary>
        private static IList<Player> GetPlayers(int nbPlayers)
        {
            var result = new List<Player>();
            for (int i = 1; i <= nbPlayers; i++)
            {
                var year = RandomGenerator.Int(1970, 2013);
                var gender = RandomGenerator.Enum<GenderType>();
                var player = new Player()
                {
                    Id = i,
                    AddressId = i,
                    Birthdate = new DateTime(year, RandomGenerator.Int(1, 12), RandomGenerator.Int(1, 27)),
                    FromDate = new DateTime(RandomGenerator.Int(year + 6, DateTime.Today.Year - 1), 8, 1),
                    CountryId = 76,
                    FirstName = NameGenerator.GenerateFirstName(gender).FirstCharToUpper(),
                    Gender = gender,
                    LastName = NameGenerator.GenerateLastName().FirstCharToUpper(),
                    Laterality = RandomGenerator.Enum<Laterality>(),
                    PlaceOfBirth = RandomGenerator.String2(5, 20).FirstCharToUpper(),
                    ShoesSize = RandomGenerator.Number(30, 50),
                    Height = RandomGenerator.Number(130, 200),
                    Weight = RandomGenerator.Number(40, 100),
                    Size = RandomGenerator.ArrayElement(PlayerConstants.DefaultSizes),
                    LicenseNumber = string.Join("", RandomGenerator.Digits(10)),
                    CreatedBy = CreatedUser,
                    CreatedDate = DateTime.Now
                };

                result.Add(player);
            }

            return result;
        }

        /// <summary>
        /// Seeds addresses.
        /// </summary>
        private static IList<Address> GetAddresses(int nbPlayers)
        {
            var result = new List<Address>();
            for (int i = 1; i <= nbPlayers; i++)
            {
                var address = new Address
                {
                    Id = i,
                    Row1 = "Rue " + RandomGenerator.String2(5, 20).FirstCharToUpper(),
                    PostalCode = RandomGenerator.String2(5, "0123456789"),
                    City = RandomGenerator.String2(5, 20).FirstCharToUpper(),
                    CreatedBy = CreatedUser,
                    CreatedDate = DateTime.Now
                };

                result.Add(address);
            }

            return result;
        }

        /// <summary>
        /// Seeds contacts.
        /// </summary>
        private static IList<Contact> GetContacts(IList<Player> players)
        {
            var id = 1;
            var result = new List<Contact>();
            foreach (var player in players)
            {
                var nbPhones = RandomGenerator.Int(0, 3);

                for (int i = 0; i < nbPhones; i++)
                {
                    var phone = new Phone
                    {
                        Id = id,
                        PersonId = player.Id,
                        Label = string.Empty,
                        Default = RandomGenerator.Bool(),
                        Value = RandomGenerator.PhoneNumber(),
                        CreatedBy = CreatedUser,
                        CreatedDate = DateTime.Now
                    };

                    id++;
                    result.Add(phone);
                }

                var nbMails = RandomGenerator.Int(0, 3);

                for (int i = 0; i < nbMails; i++)
                {
                    var mail = new Email
                    {
                        Id = id,
                        PersonId = player.Id,
                        Label = string.Empty,
                        Default = RandomGenerator.Bool(),
                        Value = player.FirstName.ToLower() + "." + player.LastName.ToLower() + i + "@coachmanager.com",
                        CreatedBy = CreatedUser,
                        CreatedDate = DateTime.Now
                    };

                    id++;
                    result.Add(mail);
                }
            }

            return result;
        }

        /// <summary>
        /// Seeds contacts.
        /// </summary>
        private static IList<Injury> GetInjuries(IList<Player> players)
        {
            var id = 1;
            var result = new List<Injury>();
            foreach (var player in players)
            {
                var nbInjuries = RandomGenerator.Int(0, 7);

                for (int i = 0; i < nbInjuries; i++)
                {
                    if (player.FromDate.HasValue)
                    {
                        var type = RandomGenerator.Enum<InjuryType>();
                        var duration = RandomGenerator.Int(3, 150);
                        var severity = InjurySeverity.Serious;
                        var date = RandomGenerator.Date(player.FromDate.Value, DateTime.Today);

                        if (duration < 7) severity = InjurySeverity.Slight;
                        else if (duration < 10) severity = InjurySeverity.Minor;
                        else if (duration < 20) severity = InjurySeverity.Average;

                        var injury = new Injury
                        {
                            Id = id,
                            PlayerId = player.Id,
                            Condition = InjuryConstants.GetDefaultCondition(type),
                            Date = date,
                            ExpectedReturn = date.AddDays(duration),
                            Severity = severity,
                            Type = type
                        };

                        id++;
                        result.Add(injury);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Seeds contacts.
        /// </summary>
        private static IList<PlayerPosition> GetPlayerPositions(IList<Player> players)
        {
            var id = 1;
            var result = new List<PlayerPosition>();
            foreach (var player in players)
            {
                var positionId = RandomGenerator.Int(1, 17);
                var position = new PlayerPosition
                {
                    Id = id,
                    PlayerId = player.Id,
                    IsNatural = true,
                    PositionId = positionId,
                    Rating = PositionConstants.MaxRating,
                    CreatedBy = CreatedUser,
                    CreatedDate = DateTime.Now
                };

                id++;
                result.Add(position);

                var nbPositions = RandomGenerator.Int(0, 3);

                for (int i = 0; i < nbPositions; i++)
                {
                    positionId = RandomGenerator.Int(1, 17);
                    if (!result.Any(x => x.PlayerId == player.Id && x.PositionId == positionId))
                    {
                        position = new PlayerPosition
                        {
                            Id = id,
                            PlayerId = player.Id,
                            IsNatural = false,
                            PositionId = positionId,
                            Rating = RandomGenerator.Int(1, PositionConstants.MaxRating - 1),
                            CreatedBy = CreatedUser,
                            CreatedDate = DateTime.Now
                        };

                        id++;
                        result.Add(position);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Seeds players.
        /// </summary>
        private static IList<Roster> GetRosters(IList<Category> categories, int nbRosters)
        {
            var result = new List<Roster>();
            for (int i = 1; i <= nbRosters; i++)
            {
                var seasonId = RandomGenerator.Number(1, 2);
                var categoryId =
                    RandomGenerator.ListItem(categories.Where(x => !result.Any(r => r.SeasonId == seasonId && r.CategoryId == x.Id)).ToList()).Id;
                var roster = new Roster
                {
                    Id = i,
                    CategoryId = categoryId,
                    Name = "Effectif " + i,
                    SeasonId = seasonId,
                    CreatedBy = CreatedUser,
                    CreatedDate = DateTime.Now
                };

                result.Add(roster);
            }

            return result;
        }

        /// <summary>
        /// Seeds players.
        /// </summary>
        private static IList<Squad> GetSquads(IList<Roster> rosters)
        {
            var id = 1;
            var result = new List<Squad>();
            foreach (var roster in rosters)
            {
                var nbSquads = RandomGenerator.Int(1, 3);

                for (int i = 1; i <= nbSquads; i++)
                {
                    var squad = new Squad
                    {
                        Id = id,
                        RosterId = roster.Id,
                        Name = "Equipe " + i,
                        CreatedBy = CreatedUser,
                        CreatedDate = DateTime.Now
                    };

                    id++;
                    result.Add(squad);
                }
            }

            return result;
        }

        /// <summary>
        /// Seeds contacts.
        /// </summary>
        private static IList<RosterPlayer> GetRosterPlayers(IList<Player> players, IList<Roster> rosters, IList<Squad> squads, IList<Season> seasons, IList<Category> categories)
        {
            var id = 1;
            var result = new List<RosterPlayer>();

            foreach (var roster in rosters)
            {
                var squadsInRoster = squads.Where(x => x.RosterId == roster.Id).ToList();
                var nbPlayersInRoster = RandomGenerator.Int(15, 20) * squadsInRoster.Count;
                var categoriesIdAllowed = new List<int?> {roster.CategoryId, roster.CategoryId + 1};

                for (int i = 0; i < nbPlayersInRoster; i++)
                {
                    var playerIdsAllowed = players
                        .Where(x =>!result.Where(p => p.RosterId == roster.Id).Select(p => p.PlayerId).Contains(x.Id))
                        .Where(x => x.Birthdate.HasValue && categoriesIdAllowed.Contains(CalculateCategoryFromDateForSeason(x.Birthdate.Value, seasons.First(s => s.Id == roster.SeasonId), categories)))
                        .ToList();

                    if(playerIdsAllowed.Count > 0) { 
                        var player = RandomGenerator.ListItem(playerIdsAllowed);
                        var squadId = RandomGenerator.ListItem(squadsInRoster).Id;

                        var rosterPlayer = new RosterPlayer
                        {
                            Id = id,
                            RosterId = roster.Id,
                            PlayerId = player.Id,
                            IsMutation = RandomGenerator.Bool(),
                            LicenseState = RandomGenerator.Enum<LicenseState>(),
                            Number = RandomGenerator.Number(1, nbPlayersInRoster),
                            CategoryId = player.Birthdate.HasValue ? CalculateCategoryFromDateForSeason(player.Birthdate.Value, seasons.First(s => s.Id == roster.SeasonId), categories) : roster.CategoryId,
                            SquadId = squadId,
                            CreatedBy = CreatedUser,
                            CreatedDate = DateTime.Now
                        };

                        id++;
                        result.Add(rosterPlayer);
                    }
                }
            }

            return result;
        }

        private static IList<Training> GetTrainings(IList<Roster> rosters, IList<Season> seasons)
        {
            var id = 1;
            var result = new List<Training>();

            foreach (var roster in rosters)
            {
                var season = seasons.First(x => x.Id == roster.SeasonId);
                IList<DateTime> dates = new List<DateTime>();
                var days = new List<DayOfWeek> {DayOfWeek.Wednesday, DayOfWeek.Friday};

                if (season.StartDate != null && season.EndDate != null)
                {
                    for (DateTime date = season.StartDate.Value;
                        date < season.EndDate.Value;
                        date = date.AddDays(1))
                    {
                        if (days.Contains(date.DayOfWeek))
                        {
                            dates.Add(date);
                        }
                    }
                }

                foreach (var date in dates)
                {
                    var training = new Training
                    {
                        Id = id,
                        StartDate = new DateTime(date.Year, date.Month, date.Day, 17, 30, 0),
                        Place = String.Empty,
                        IsCancelled = false,
                        RosterId = roster.Id,
                        Stage = Stage.Other,
                        Theme = RandomGenerator.String2(10,50),
                        EndDate = new DateTime(date.Year, date.Month, date.Day, 19, 0, 0),
                        CreatedBy = CreatedUser,
                        CreatedDate = DateTime.Now
                    };
                    
                    id++;
                    result.Add(training);
                }
            }

            return result;
        }

        private static IList<TrainingAttendance> GetTrainingsAttendances(IList<Training> trainings, IList<RosterPlayer> rosterPlayers)
        {
            var id = 1;
            var result = new List<TrainingAttendance>();

            foreach (var training in trainings)
            {
                var players = rosterPlayers.Where(x => x.RosterId == training.RosterId).ToList();

                foreach (var player in players)
                {
                    var attendance = RandomGenerator.Enum<Attendance>();
                    var trainingAttendance = new TrainingAttendance
                    {
                        Id = id,
                        Attendance = attendance,
                        Reason = attendance == Attendance.Apology ? RandomGenerator.String2(30,100) : string.Empty,
                        RosterPlayerId = player.Id,
                        TrainingId = training.Id
                    };
                    id++;
                    result.Add(trainingAttendance);
                }
            }

            return result;
        }

        /// <summary>
            /// Seeds users and permissions.
            /// </summary>
            private static IList<User> GetUsers()
        {
            var result = new List<User>
            {
               new User() {Id=1, Name = "Stéphane ANDRE (Home)", Login = "andre", Password = "qRBfE9MoPFs=", Mail = "andre.cs2i@gmail.com", CreatedBy = CreatedUser, CreatedDate = DateTime.Now},
            new User() {Id=2, Name = "Stéphane ANDRE (Sopra)", Login = "sandre", Password = "qRBfE9MoPFs=", Mail = "stephane.andre@ext.soprasteria.com", CreatedBy = CreatedUser, CreatedDate = DateTime.Now}
            };

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="date"></param>
        /// <param name="season"></param>
        /// <param name="categories"></param>
        /// <returns></returns>
        private static int? CalculateCategoryFromDateForSeason(DateTime date, Season season, IEnumerable<Category> categories)
        {
            var toDate = season.StartDate ?? DateTime.Today;
            var diffYear = toDate.Year - date.Year;
            var category = categories.Where(x => x.Age <= diffYear).OrderByDescending(x => x.Age).FirstOrDefault();

            return category?.Id;
        }
    }
}
