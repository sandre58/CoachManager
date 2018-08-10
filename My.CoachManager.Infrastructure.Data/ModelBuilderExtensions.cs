﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using My.CoachManager.CrossCutting.Core.Enums;
using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Infrastructure.Data
{
    public static class ModelBuilderExtensions
    {
        /// <summary>
        /// Seeds data.
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(GetCategories().ToArray());
            modelBuilder.Entity<Season>().HasData(GetSeasons().ToArray());
            modelBuilder.Entity<Country>().HasData(GetCountries().ToArray());
            modelBuilder.Entity<Address>().HasData(GetAddresses().ToArray());
            modelBuilder.Entity<Player>().HasData(GetPlayers().ToArray());
            modelBuilder.Entity<Email>().HasData(GetContacts().OfType<Email>().ToArray());
            modelBuilder.Entity<Phone>().HasData(GetContacts().OfType<Phone>().ToArray());
            modelBuilder.Entity<User>().HasData(GetUsers().ToArray());
        }

        /// <summary>
        /// Seeds categories.
        /// </summary>
        private static IList<Category> GetCategories()
        {
            var result = new List<Category>
            {
                new Category() {Id = 1, Label = "Séniors", Code = "S", Year = 1998, Order = 1},
                new Category() {Id = 2, Label = "Vétérans", Code = "V", Year = 1982, Order = 2}
            };

            var year = 1999;
            var order = 3;
            for (var i = 19; i >= 6; i--)
            {
                var code = "U" + i;
                result.Add(new Category() { Id = order, Label = code, Code = code, Year = year, Order = order });
                year++;
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
                new Season() {Id = 1, Label = "2017/2018", Code = "17/18", Order = 1, StartDate = new DateTime(2017, 08, 01), EndDate = new DateTime(2018, 07, 31)},
                new Season() {Id = 2, Label = "2018/2019", Code = "18/19", Order = 1, StartDate = new DateTime(2018, 08, 01), EndDate = new DateTime(2019, 07, 31)}
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
            new Country() {Id = 1, Label = "Afghanistan", Code = "afg", Flag = "af.png" },
            new Country() {Id = 2, Label = "Aland Islands", Code = "ala", Flag = "ax.png" },
            new Country() {Id = 3, Label = "Albania", Code = "alb", Flag = "al.png" },
            new Country() {Id = 4, Label = "Algeria", Code = "dza", Flag = "dz.png" },
            new Country() {Id = 5, Label = "American Samoa", Code = "asm", Flag = "as.png" },
            new Country() {Id = 6, Label = "Andorra", Code = "and", Flag = "ad.png" },
            new Country() {Id = 7, Label = "Angola", Code = "ago", Flag = "ao.png" },
            new Country() {Id = 8, Label = "Anguilla", Code = "aia", Flag = "ai.png" },
            new Country() {Id = 9, Label = "Antarctica", Code = "ant", Flag = "aq.png" },
            new Country() {Id = 10, Label = "Antigua and Barbuda", Code = "atg", Flag = "ag.png" },
            new Country() {Id = 11, Label = "Argentina", Code = "arg", Flag = "ar.png" },
            new Country() {Id = 12, Label = "Armenia", Code = "arm", Flag = "am.png" },
            new Country() {Id = 13, Label = "Aruba", Code = "abw", Flag = "aw.png" },
            new Country() {Id = 14, Label = "Australia", Code = "aus", Flag = "au.png" },
            new Country() {Id = 15, Label = "Austria", Code = "aut", Flag = "at.png" },
            new Country() {Id = 16, Label = "Azerbaijan", Code = "aze", Flag = "az.png" },
            new Country() {Id = 17, Label = "Bahamas", Code = "bhs", Flag = "bs.png" },
            new Country() {Id = 18, Label = "Bahrain", Code = "bhr", Flag = "bh.png" },
            new Country() {Id = 19, Label = "Bangladesh", Code = "bgd", Flag = "bd.png" },
            new Country() {Id = 20, Label = "Barbados", Code = "brb", Flag = "bb.png" },
            new Country() {Id = 21, Label = "Belarus", Code = "blr", Flag = "by.png" },
            new Country() {Id = 22, Label = "Belgium", Code = "bel", Flag = "be.png" },
            new Country() {Id = 23, Label = "Belize", Code = "blz", Flag = "bz.png" },
            new Country() {Id = 24, Label = "Benin", Code = "ben", Flag = "bj.png" },
            new Country() {Id = 25, Label = "Bermuda", Code = "bmu", Flag = "bm.png" },
            new Country() {Id = 26, Label = "Bhutan", Code = "btn", Flag = "bt.png" },
            new Country() {Id = 27, Label = "Bolivia, Plurinational State of", Code = "bol", Flag = "bo.png" },
            new Country() {Id = 28, Label = "Bonaire, Sint Eustatius and Saba", Code = "bes", Flag = "bq.png" },
            new Country() {Id = 29, Label = "Bosnia and Herzegovina", Code = "bih", Flag = "ba.png" },
            new Country() {Id = 30, Label = "Botswana", Code = "bwa", Flag = "bw.png" },
            new Country() {Id = 31, Label = "Bouvet Island", Code = "bvi", Flag = "bv.png" },
            new Country() {Id = 32, Label = "Brazil", Code = "bra", Flag = "br.png" },
            new Country() {Id = 33, Label = "British Indian Ocean Territory", Code = "bio", Flag = "io.png" },
            new Country() {Id = 34, Label = "Brunei Darussalam", Code = "brn", Flag = "bn.png" },
            new Country() {Id = 35, Label = "Bulgaria", Code = "bgr", Flag = "bg.png" },
            new Country() {Id = 36, Label = "Burkina Faso", Code = "bfa", Flag = "bf.png" },
            new Country() {Id = 37, Label = "Burundi", Code = "bdi", Flag = "bi.png" },
            new Country() {Id = 38, Label = "Cambodia", Code = "khm", Flag = "kh.png" },
            new Country() {Id = 39, Label = "Cameroon", Code = "cmr", Flag = "cm.png" },
            new Country() {Id = 40, Label = "Canada", Code = "can", Flag = "ca.png" },
            new Country() {Id = 41, Label = "Cape Verde", Code = "cpv", Flag = "cv.png" },
            new Country() {Id = 42, Label = "Cayman Islands", Code = "cym", Flag = "ky.png" },
            new Country() {Id = 43, Label = "Central African Republic", Code = "caf", Flag = "cf.png" },
            new Country() {Id = 44, Label = "Chad", Code = "tcd", Flag = "td.png" },
            new Country() {Id = 45, Label = "Chile", Code = "chl", Flag = "cl.png" },
            new Country() {Id = 46, Label = "China", Code = "chn", Flag = "cn.png" },
            new Country() {Id = 47, Label = "Christmas Island", Code = "chi", Flag = "cx.png" },
            new Country() {Id = 48, Label = "Cocos (Keeling) Islands", Code = "coc", Flag = "cc.png" },
            new Country() {Id = 49, Label = "Colombia", Code = "col", Flag = "co.png" },
            new Country() {Id = 50, Label = "Comoros", Code = "com", Flag = "km.png" },
            new Country() {Id = 51, Label = "Congo", Code = "cog", Flag = "cg.png" },
            new Country() {Id = 52, Label = "Congo, The Democratic Republic of the", Code = "cod", Flag = "cd.png" },
            new Country() {Id = 53, Label = "Cook Islands", Code = "cok", Flag = "ck.png" },
            new Country() {Id = 54, Label = "Costa Rica", Code = "cri", Flag = "cr.png" },
            new Country() {Id = 55, Label = "Cote d'Ivoire", Code = "civ", Flag = "ci.png" },
            new Country() {Id = 56, Label = "Croatia", Code = "hrv", Flag = "hr.png" },
            new Country() {Id = 57, Label = "Cuba", Code = "cub", Flag = "cu.png" },
            new Country() {Id = 58, Label = "Curacao", Code = "cuw", Flag = "cw.png" },
            new Country() {Id = 59, Label = "Cyprus", Code = "cyp", Flag = "cy.png" },
            new Country() {Id = 60, Label = "Czech Republic", Code = "cze", Flag = "cz.png" },
            new Country() {Id = 61, Label = "Denmark", Code = "dnk", Flag = "dk.png" },
            new Country() {Id = 62, Label = "Djibouti", Code = "dji", Flag = "dj.png" },
            new Country() {Id = 63, Label = "Dominica", Code = "dma", Flag = "dm.png" },
            new Country() {Id = 64, Label = "Dominican Republic", Code = "dom", Flag = "do.png" },
            new Country() {Id = 65, Label = "Ecuador", Code = "ecu", Flag = "ec.png" },
            new Country() {Id = 66, Label = "Egypt", Code = "egy", Flag = "eg.png" },
            new Country() {Id = 67, Label = "El Salvador", Code = "slv", Flag = "sv.png" },
            new Country() {Id = 68, Label = "Equatorial Guinea", Code = "gnq", Flag = "gq.png" },
            new Country() {Id = 69, Label = "Eritrea", Code = "eri", Flag = "er.png" },
            new Country() {Id = 70, Label = "Estonia", Code = "est", Flag = "ee.png" },
            new Country() {Id = 71, Label = "Ethiopia", Code = "eth", Flag = "et.png" },
            new Country() {Id = 72, Label = "Falkland Islands (Malvinas)", Code = "flk", Flag = "fk.png" },
            new Country() {Id = 73, Label = "Faroe Islands", Code = "fro", Flag = "fo.png" },
            new Country() {Id = 74, Label = "Fiji", Code = "fji", Flag = "fj.png" },
            new Country() {Id = 75, Label = "Finland", Code = "fin", Flag = "fi.png" },
            new Country() {Id = 76, Label = "France", Code = "fra", Flag = "fr.png" },
            new Country() {Id = 77, Label = "French Guiana", Code = "guf", Flag = "gf.png" },
            new Country() {Id = 78, Label = "French Polynesia", Code = "pyf", Flag = "pf.png" },
            new Country() {Id = 79, Label = "French Southern Territories", Code = "fst", Flag = "tf.png" },
            new Country() {Id = 80, Label = "Gabon", Code = "gab", Flag = "ga.png" },
            new Country() {Id = 81, Label = "Gambia", Code = "gmb", Flag = "gm.png" },
            new Country() {Id = 82, Label = "Georgia", Code = "geo", Flag = "ge.png" },
            new Country() {Id = 83, Label = "Germany", Code = "deu", Flag = "de.png" },
            new Country() {Id = 84, Label = "Ghana", Code = "gha", Flag = "gh.png" },
            new Country() {Id = 85, Label = "Gibraltar", Code = "gib", Flag = "gi.png" },
            new Country() {Id = 86, Label = "Greece", Code = "grc", Flag = "gr.png" },
            new Country() {Id = 87, Label = "Greenland", Code = "grl", Flag = "gl.png" },
            new Country() {Id = 88, Label = "Grenada", Code = "grd", Flag = "gd.png" },
            new Country() {Id = 89, Label = "Guadeloupe", Code = "glp", Flag = "gp.png" },
            new Country() {Id = 90, Label = "Guam", Code = "gum", Flag = "gu.png" },
            new Country() {Id = 91, Label = "Guatemala", Code = "gtm", Flag = "gt.png" },
            new Country() {Id = 92, Label = "Guernsey", Code = "ggy", Flag = "gg.png" },
            new Country() {Id = 93, Label = "Guinea", Code = "gin", Flag = "gn.png" },
            new Country() {Id = 94, Label = "Guinea-Bissau", Code = "gnb", Flag = "gw.png" },
            new Country() {Id = 95, Label = "Guyana", Code = "guy", Flag = "gy.png" },
            new Country() {Id = 96, Label = "Haiti", Code = "hti", Flag = "ht.png" },
            new Country() {Id = 97, Label = "Heard Island and McDonald Islands", Code = "him", Flag = "hm.png" },
            new Country() {Id = 98, Label = "Holy See (Vatican City State)", Code = "vat", Flag = "va.png" },
            new Country() {Id = 99, Label = "Honduras", Code = "hnd", Flag = "hn.png" },
            new Country() {Id = 100, Label = "Hong Kong", Code = "hkg", Flag = "hk.png" },
            new Country() {Id = 101, Label = "Hungary", Code = "hun", Flag = "hu.png" },
            new Country() {Id = 102, Label = "Iceland", Code = "isl", Flag = "is.png" },
            new Country() {Id = 103, Label = "India", Code = "ind", Flag = "in.png" },
            new Country() {Id = 104, Label = "Indonesia", Code = "idn", Flag = "id.png" },
            new Country() {Id = 105, Label = "Iran, Islamic Republic of", Code = "irn", Flag = "ir.png" },
            new Country() {Id = 106, Label = "Iraq", Code = "irq", Flag = "iq.png" },
            new Country() {Id = 107, Label = "Ireland", Code = "irl", Flag = "ie.png" },
            new Country() {Id = 108, Label = "Isle of Man", Code = "imn", Flag = "im.png" },
            new Country() {Id = 109, Label = "Israel", Code = "isr", Flag = "il.png" },
            new Country() {Id = 110, Label = "Italy", Code = "ita", Flag = "it.png" },
            new Country() {Id = 111, Label = "Jamaica", Code = "jam", Flag = "jm.png" },
            new Country() {Id = 112, Label = "Japan", Code = "jpn", Flag = "jp.png" },
            new Country() {Id = 113, Label = "Jersey", Code = "jey", Flag = "je.png" },
            new Country() {Id = 114, Label = "Jordan", Code = "jor", Flag = "jo.png" },
            new Country() {Id = 115, Label = "Kazakhstan", Code = "kaz", Flag = "kz.png" },
            new Country() {Id = 116, Label = "Kenya", Code = "ken", Flag = "ke.png" },
            new Country() {Id = 117, Label = "Kiribati", Code = "kir", Flag = "ki.png" },
            new Country() {Id = 118, Label = "Korea, Democratic People's Republic of", Code = "prk", Flag = "kp.png" },
            new Country() {Id = 119, Label = "Korea, Republic of", Code = "kor", Flag = "kr.png" },
            new Country() {Id = 120, Label = "Kuwait", Code = "kwt", Flag = "kw.png" },
            new Country() {Id = 121, Label = "Kyrgyzstan", Code = "kgz", Flag = "kg.png" },
            new Country() {Id = 122, Label = "Lao People's Democratic Republic", Code = "lao", Flag = "la.png" },
            new Country() {Id = 123, Label = "Latvia", Code = "lva", Flag = "lv.png" },
            new Country() {Id = 124, Label = "Lebanon", Code = "lbn", Flag = "lb.png" },
            new Country() {Id = 125, Label = "Lesotho", Code = "lso", Flag = "ls.png" },
            new Country() {Id = 126, Label = "Liberia", Code = "lbr", Flag = "lr.png" },
            new Country() {Id = 127, Label = "Libyan Arab Jamahiriya", Code = "lby", Flag = "ly.png" },
            new Country() {Id = 128, Label = "Liechtenstein", Code = "lie", Flag = "li.png" },
            new Country() {Id = 129, Label = "Lithuania", Code = "ltu", Flag = "lt.png" },
            new Country() {Id = 130, Label = "Luxembourg", Code = "lux", Flag = "lu.png" },
            new Country() {Id = 131, Label = "Macao", Code = "mac", Flag = "mo.png" },
            new Country() {Id = 132, Label = "Macedonia, The former Yugoslav Republic of", Code = "mkd", Flag = "mk.png" },
            new Country() {Id = 133, Label = "Madagascar", Code = "mdg", Flag = "mg.png" },
            new Country() {Id = 134, Label = "Malawi", Code = "mwi", Flag = "mw.png" },
            new Country() {Id = 135, Label = "Malaysia", Code = "mys", Flag = "my.png" },
            new Country() {Id = 136, Label = "Maldives", Code = "mdv", Flag = "mv.png" },
            new Country() {Id = 137, Label = "Mali", Code = "mli", Flag = "ml.png" },
            new Country() {Id = 138, Label = "Malta", Code = "mlt", Flag = "mt.png" },
            new Country() {Id = 139, Label = "Marshall Islands", Code = "mhl", Flag = "mh.png" },
            new Country() {Id = 140, Label = "Martinique", Code = "mtq", Flag = "mq.png" },
            new Country() {Id = 141, Label = "Mauritania", Code = "mrt", Flag = "mr.png" },
            new Country() {Id = 142, Label = "Mauritius", Code = "mus", Flag = "mu.png" },
            new Country() {Id = 143, Label = "Mayotte", Code = "myt", Flag = "yt.png" },
            new Country() {Id = 144, Label = "Mexico", Code = "mex", Flag = "mx.png" },
            new Country() {Id = 145, Label = "Micronesia, Federated States of", Code = "fsm", Flag = "fm.png" },
            new Country() {Id = 146, Label = "Moldova, Republic of", Code = "mda", Flag = "md.png" },
            new Country() {Id = 147, Label = "Monaco", Code = "mco", Flag = "mc.png" },
            new Country() {Id = 148, Label = "Mongolia", Code = "mng", Flag = "mn.png" },
            new Country() {Id = 149, Label = "Montenegro", Code = "mne", Flag = "me.png" },
            new Country() {Id = 150, Label = "Montserrat", Code = "msr", Flag = "ms.png" },
            new Country() {Id = 151, Label = "Morocco", Code = "mar", Flag = "ma.png" },
            new Country() {Id = 152, Label = "Mozambique", Code = "moz", Flag = "mz.png" },
            new Country() {Id = 153, Label = "Myanmar", Code = "mmr", Flag = "mm.png" },
            new Country() {Id = 154, Label = "Namibia", Code = "nam", Flag = "na.png" },
            new Country() {Id = 155, Label = "Nauru", Code = "nru", Flag = "nr.png" },
            new Country() {Id = 156, Label = "Nepal", Code = "npl", Flag = "np.png" },
            new Country() {Id = 157, Label = "Netherlands", Code = "nld", Flag = "nl.png" },
            new Country() {Id = 158, Label = "New Caledonia", Code = "ncl", Flag = "nc.png" },
            new Country() {Id = 159, Label = "New Zealand", Code = "nzl", Flag = "nz.png" },
            new Country() {Id = 160, Label = "Nicaragua", Code = "nic", Flag = "ni.png" },
            new Country() {Id = 161, Label = "Niger", Code = "ner", Flag = "ne.png" },
            new Country() {Id = 162, Label = "Nigeria", Code = "nga", Flag = "ng.png" },
            new Country() {Id = 163, Label = "Niue", Code = "niu", Flag = "nu.png" },
            new Country() {Id = 164, Label = "Norfolk Island", Code = "nfk", Flag = "nf.png" },
            new Country() {Id = 165, Label = "Northern Mariana Islands", Code = "mnp", Flag = "mp.png" },
            new Country() {Id = 166, Label = "Norway", Code = "nor", Flag = "no.png" },
            new Country() {Id = 167, Label = "Oman", Code = "omn", Flag = "om.png" },
            new Country() {Id = 168, Label = "Pakistan", Code = "pak", Flag = "pk.png" },
            new Country() {Id = 169, Label = "Palau", Code = "plw", Flag = "pw.png" },
            new Country() {Id = 170, Label = "Palestinian Territory, Occupied", Code = "pse", Flag = "ps.png" },
            new Country() {Id = 171, Label = "Panama", Code = "pan", Flag = "pa.png" },
            new Country() {Id = 172, Label = "Papua New Guinea", Code = "png", Flag = "pg.png" },
            new Country() {Id = 173, Label = "Paraguay", Code = "pry", Flag = "py.png" },
            new Country() {Id = 174, Label = "Peru", Code = "per", Flag = "pe.png" },
            new Country() {Id = 175, Label = "Philippines", Code = "phl", Flag = "ph.png" },
            new Country() {Id = 176, Label = "Pitcairn", Code = "pcn", Flag = "pn.png" },
            new Country() {Id = 177, Label = "Poland", Code = "pol", Flag = "pl.png" },
            new Country() {Id = 178, Label = "Portugal", Code = "prt", Flag = "pt.png" },
            new Country() {Id = 179, Label = "Puerto Rico", Code = "pri", Flag = "pr.png" },
            new Country() {Id = 180, Label = "Qatar", Code = "qat", Flag = "qa.png" },
            new Country() {Id = 181, Label = "Reunion", Code = "reu", Flag = "re.png" },
            new Country() {Id = 182, Label = "Romania", Code = "rou", Flag = "ro.png" },
            new Country() {Id = 183, Label = "Russian Federation", Code = "rus", Flag = "ru.png" },
            new Country() {Id = 184, Label = "Rwanda", Code = "rwa", Flag = "rw.png" },
            new Country() {Id = 185, Label = "Saint Barthelemy", Code = "blm", Flag = "bl.png" },
            new Country() {Id = 186, Label = "Saint Helena, Ascension and Tristan Da Cunha", Code = "shn", Flag = "sh.png" },
            new Country() {Id = 187, Label = "Saint Kitts and Nevis", Code = "kna", Flag = "kn.png" },
            new Country() {Id = 188, Label = "Saint Lucia", Code = "lca", Flag = "lc.png" },
            new Country() {Id = 189, Label = "Saint Martin (French Part)", Code = "maf", Flag = "mf.png" },
            new Country() {Id = 190, Label = "Saint Pierre and Miquelon", Code = "spm", Flag = "pm.png" },
            new Country() {Id = 191, Label = "Saint Vincent and The Grenadines", Code = "vct", Flag = "vc.png" },
            new Country() {Id = 192, Label = "Samoa", Code = "wsm", Flag = "ws.png" },
            new Country() {Id = 193, Label = "San Marino", Code = "smr", Flag = "sm.png" },
            new Country() {Id = 194, Label = "Sao Tome and Principe", Code = "stp", Flag = "st.png" },
            new Country() {Id = 195, Label = "Saudi Arabia", Code = "sau", Flag = "sa.png" },
            new Country() {Id = 196, Label = "Senegal", Code = "sen", Flag = "sn.png" },
            new Country() {Id = 197, Label = "Serbia", Code = "srb", Flag = "rs.png" },
            new Country() {Id = 198, Label = "Seychelles", Code = "syc", Flag = "sc.png" },
            new Country() {Id = 199, Label = "Sierra Leone", Code = "sle", Flag = "sl.png" },
            new Country() {Id = 200, Label = "Singapore", Code = "sgp", Flag = "sg.png" },
            new Country() {Id = 201, Label = "Sint Maarten (Dutch Part)", Code = "sxm", Flag = "sx.png" },
            new Country() {Id = 202, Label = "Slovakia", Code = "svk", Flag = "sk.png" },
            new Country() {Id = 203, Label = "Slovenia", Code = "svn", Flag = "si.png" },
            new Country() {Id = 204, Label = "Solomon Islands", Code = "slb", Flag = "sb.png" },
            new Country() {Id = 205, Label = "Somalia", Code = "som", Flag = "so.png" },
            new Country() {Id = 206, Label = "South Africa", Code = "zaf", Flag = "za.png" },
            new Country() {Id = 207, Label = "South Georgia and The South Sandwich Islands", Code = "sgt", Flag = "gs.png" },
            new Country() {Id = 208, Label = "South Sudan", Code = "ssd", Flag = "ss.png" },
            new Country() {Id = 209, Label = "Spain", Code = "esp", Flag = "es.png" },
            new Country() {Id = 210, Label = "Sri Lanka", Code = "lka", Flag = "lk.png" },
            new Country() {Id = 211, Label = "Sudan", Code = "sdn", Flag = "sd.png" },
            new Country() {Id = 212, Label = "Suriname", Code = "sur", Flag = "sr.png" },
            new Country() {Id = 213, Label = "Svalbard and Jan Mayen", Code = "sjm", Flag = "sj.png" },
            new Country() {Id = 214, Label = "Swaziland", Code = "swz", Flag = "sz.png" },
            new Country() {Id = 215, Label = "Sweden", Code = "swe", Flag = "se.png" },
            new Country() {Id = 216, Label = "Switzerland", Code = "che", Flag = "ch.png" },
            new Country() {Id = 217, Label = "Syrian Arab Republic", Code = "syr", Flag = "sy.png" },
            new Country() {Id = 218, Label = "Taiwan, Province of China", Code = "tpc", Flag = "tw.png" },
            new Country() {Id = 219, Label = "Tajikistan", Code = "tjk", Flag = "tj.png" },
            new Country() {Id = 220, Label = "Tanzania, United Republic of", Code = "tza", Flag = "tz.png" },
            new Country() {Id = 221, Label = "Thailand", Code = "tha", Flag = "th.png" },
            new Country() {Id = 222, Label = "Timor-Leste", Code = "tls", Flag = "tl.png" },
            new Country() {Id = 223, Label = "Togo", Code = "tgo", Flag = "tg.png" },
            new Country() {Id = 224, Label = "Tokelau", Code = "tkl", Flag = "tk.png" },
            new Country() {Id = 225, Label = "Tonga", Code = "ton", Flag = "to.png" },
            new Country() {Id = 226, Label = "Trinidad and Tobago", Code = "tto", Flag = "tt.png" },
            new Country() {Id = 227, Label = "Tunisia", Code = "tun", Flag = "tn.png" },
            new Country() {Id = 228, Label = "Turkey", Code = "tur", Flag = "tr.png" },
            new Country() {Id = 229, Label = "Turkmenistan", Code = "tkm", Flag = "tm.png" },
            new Country() {Id = 230, Label = "Turks and Caicos Islands", Code = "tca", Flag = "tc.png" },
            new Country() {Id = 231, Label = "Tuvalu", Code = "tuv", Flag = "tv.png" },
            new Country() {Id = 232, Label = "Uganda", Code = "uga", Flag = "ug.png" },
            new Country() {Id = 233, Label = "Ukraine", Code = "ukr", Flag = "ua.png" },
            new Country() {Id = 234, Label = "United Arab Emirates", Code = "are", Flag = "ae.png" },
            new Country() {Id = 235, Label = "United Kingdom", Code = "gbr", Flag = "gb.png" },
            new Country() {Id = 236, Label = "United States", Code = "usa", Flag = "us.png" },
            new Country() {Id = 237, Label = "United States Minor Outlying Islands", Code = "usm", Flag = "um.png" },
            new Country() {Id = 238, Label = "Uruguay", Code = "ury", Flag = "uy.png" },
            new Country() {Id = 239, Label = "Uzbekistan", Code = "uzb", Flag = "uz.png" },
            new Country() {Id = 240, Label = "Vanuatu", Code = "vut", Flag = "vu.png" },
            new Country() {Id = 241, Label = "Venezuela, Bolivarian Republic of", Code = "ven", Flag = "ve.png" },
            new Country() {Id = 242, Label = "Viet Nam", Code = "vnm", Flag = "vn.png" },
            new Country() {Id = 243, Label = "Virgin Islands, British", Code = "vgb", Flag = "vg.png" },
            new Country() {Id = 244, Label = "Virgin Islands, U.S.", Code = "vir", Flag = "vi.png" },
            new Country() {Id = 245, Label = "Wallis and Futuna", Code = "wlf", Flag = "wf.png" },
            new Country() {Id = 246, Label = "Western Sahara", Code = "esh", Flag = "eh.png" },
            new Country() {Id = 247, Label = "Yemen", Code = "yem", Flag = "ye.png" },
            new Country() {Id = 248, Label = "Zambia", Code = "zmb", Flag = "zm.png" },
            new Country() {Id = 249, Label = "Zimbabwe", Code = "zwe", Flag = "zw.png" },
            };

            return result;
        }

        /// <summary>
        /// Seeds players.
        /// </summary>
        private static IList<Player> GetPlayers()
        {
            var result = new List<Player>
            {
            new Player()
            {
                Id = 1,
                CategoryId = 13,
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
            },

            new Player()
            {
                Id = 2,
                CategoryId = 3,
                Birthdate = new DateTime(1986, 12, 4),
                AddressId = 1,
                CountryId = 76,
                FirstName = "Vincent",
                Gender = GenderType.Male,
                LastName = "Sourdeix",
                Laterality = Laterality.RightHander,
                PlaceOfBirth = "Tulle",
                ShoesSize = 42,
                Size = "L",
                LicenseNumber = "123456789"
            }};

            return result;
        }

        /// <summary>
        /// Seeds addresses.
        /// </summary>
        private static IList<Address> GetAddresses()
        {
            var result = new List<Address>
            {
                new Address()
                {
                    Id = 1,
                    Row1 = "Impasse du Babory",
                    PostalCode = "63270",
                    City = "Vic le comte"
                }
            };

            return result;
        }

        /// <summary>
        /// Seeds contacts.
        /// </summary>
        private static IList<Contact> GetContacts()
        {
            var result = new List<Contact>
            {
                new Email
                {
                    Id = 1,
                    PersonId = 1,
                    Label = "Test",
                    Default = true,
                    Value = "andre.cs2i@gmail.com"
                },
                new Email
                {
                    Id = 2,
                    PersonId = 1,
                    Label = "Test2",
                    Default = false,
                    Value = "vincentsourdeix@gmail.com"
                },
                new Phone
                {
                    Id = 3,
                    PersonId = 1,
                    Label = "Test",
                    Default = true,
                    Value = "0664411391"
                },

                new Email
                {
                    Id = 4,
                    PersonId = 2,
                    Label = "Principale",
                    Default = true,
                    Value = "visourdeix@gmail.com"
                },
                new Email
                {
                    Id = 5,
                    PersonId = 2,
                    Label = "Pub",
                    Default = false,
                    Value = "vincentsourdeix@gmail.com"
                },
                new Phone
                {
                    Id = 6,
                    PersonId = 2,
                    Label = "Portable",
                    Default = true,
                    Value = "0679189256"
                }
            };

            return result;
        }

        /// <summary>
        /// Seeds users and permissions.
        /// </summary>
        private static IList<User> GetUsers()
        {
            var result = new List<User>
            {
               new User() {Id=1, Name = "Stéphane ANDRE (Home)", Login = "andre", Password = "qRBfE9MoPFs=", Mail = "andre.cs2i@gmail.com" },
            new User() {Id=2, Name = "Stéphane ANDRE (Merial)", Login = "E0214719", Password = "qRBfE9MoPFs=", Mail = "stephane.andre@merial.com" },
            new User() {Id=3, Name = "Vincent SOURDEIX (BI)", Login = "E0268620", Password = "qRBfE9MoPFs=", Mail = "vincentsourdeix@test.fr" },
            new User() {Id=4, Name = "Stéphane ANDRE (Modis)", Login = "stephane.andre", Password = "qRBfE9MoPFs=", Mail = "stephane.andre@modis.com" }
            };

            return result;
        }
    }
}