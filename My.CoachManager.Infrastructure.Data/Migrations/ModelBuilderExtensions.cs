using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Infrastructure.Data.Migrations
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
        }

        /// <summary>
        /// Seeds categories.
        /// </summary>
        private static IList<Category> GetCategories()
        {
            var result = new List<Category>
            {
                new Category() {Label = "Séniors", Code = "S", Year = 1998, Order = 1},
                new Category() {Label = "Vétérans", Code = "V", Year = 1982, Order = 2}
            };

            var year = 1999;
            var order = 3;
            for (var i = 19; i >= 6; i--)
            {
                var code = "U" + i;
                result.Add(new Category() { Label = code, Code = code, Year = year, Order = order });
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
                new Season() {Label = "2017/2018", Code = "17/18", Order = 1, StartDate = new DateTime(2017, 08, 01), EndDate = new DateTime(2018, 07, 31)},
                new Season() {Label = "2018/2019", Code = "18/19", Order = 1, StartDate = new DateTime(2018, 08, 01), EndDate = new DateTime(2019, 07, 31)}
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
            new Country() { Label = "Afghanistan", Code = "afg", Flag = "af.png" },
            new Country() { Label = "Aland Islands", Code = "ala", Flag = "ax.png" },
            new Country() { Label = "Albania", Code = "alb", Flag = "al.png" },
            new Country() { Label = "Algeria", Code = "dza", Flag = "dz.png" },
            new Country() { Label = "American Samoa", Code = "asm", Flag = "as.png" },
            new Country() { Label = "Andorra", Code = "and", Flag = "ad.png" },
            new Country() { Label = "Angola", Code = "ago", Flag = "ao.png" },
            new Country() { Label = "Anguilla", Code = "aia", Flag = "ai.png" },
            new Country() { Label = "Antarctica", Code = "ant", Flag = "aq.png" },
            new Country() { Label = "Antigua and Barbuda", Code = "atg", Flag = "ag.png" },
            new Country() { Label = "Argentina", Code = "arg", Flag = "ar.png" },
            new Country() { Label = "Armenia", Code = "arm", Flag = "am.png" },
            new Country() { Label = "Aruba", Code = "abw", Flag = "aw.png" },
            new Country() { Label = "Australia", Code = "aus", Flag = "au.png" },
            new Country() { Label = "Austria", Code = "aut", Flag = "at.png" },
            new Country() { Label = "Azerbaijan", Code = "aze", Flag = "az.png" },
            new Country() { Label = "Bahamas", Code = "bhs", Flag = "bs.png" },
            new Country() { Label = "Bahrain", Code = "bhr", Flag = "bh.png" },
            new Country() { Label = "Bangladesh", Code = "bgd", Flag = "bd.png" },
            new Country() { Label = "Barbados", Code = "brb", Flag = "bb.png" },
            new Country() { Label = "Belarus", Code = "blr", Flag = "by.png" },
            new Country() { Label = "Belgium", Code = "bel", Flag = "be.png" },
            new Country() { Label = "Belize", Code = "blz", Flag = "bz.png" },
            new Country() { Label = "Benin", Code = "ben", Flag = "bj.png" },
            new Country() { Label = "Bermuda", Code = "bmu", Flag = "bm.png" },
            new Country() { Label = "Bhutan", Code = "btn", Flag = "bt.png" },
            new Country() { Label = "Bolivia, Plurinational State of", Code = "bol", Flag = "bo.png" },
            new Country() { Label = "Bonaire, Sint Eustatius and Saba", Code = "bes", Flag = "bq.png" },
            new Country() { Label = "Bosnia and Herzegovina", Code = "bih", Flag = "ba.png" },
            new Country() { Label = "Botswana", Code = "bwa", Flag = "bw.png" },
            new Country() { Label = "Bouvet Island", Code = "bvi", Flag = "bv.png" },
            new Country() { Label = "Brazil", Code = "bra", Flag = "br.png" },
            new Country() { Label = "British Indian Ocean Territory", Code = "bio", Flag = "io.png" },
            new Country() { Label = "Brunei Darussalam", Code = "brn", Flag = "bn.png" },
            new Country() { Label = "Bulgaria", Code = "bgr", Flag = "bg.png" },
            new Country() { Label = "Burkina Faso", Code = "bfa", Flag = "bf.png" },
            new Country() { Label = "Burundi", Code = "bdi", Flag = "bi.png" },
            new Country() { Label = "Cambodia", Code = "khm", Flag = "kh.png" },
            new Country() { Label = "Cameroon", Code = "cmr", Flag = "cm.png" },
            new Country() { Label = "Canada", Code = "can", Flag = "ca.png" },
            new Country() { Label = "Cape Verde", Code = "cpv", Flag = "cv.png" },
            new Country() { Label = "Cayman Islands", Code = "cym", Flag = "ky.png" },
            new Country() { Label = "Central African Republic", Code = "caf", Flag = "cf.png" },
            new Country() { Label = "Chad", Code = "tcd", Flag = "td.png" },
            new Country() { Label = "Chile", Code = "chl", Flag = "cl.png" },
            new Country() { Label = "China", Code = "chn", Flag = "cn.png" },
            new Country() { Label = "Christmas Island", Code = "chi", Flag = "cx.png" },
            new Country() { Label = "Cocos (Keeling) Islands", Code = "coc", Flag = "cc.png" },
            new Country() { Label = "Colombia", Code = "col", Flag = "co.png" },
            new Country() { Label = "Comoros", Code = "com", Flag = "km.png" },
            new Country() { Label = "Congo", Code = "cog", Flag = "cg.png" },
            new Country() { Label = "Congo, The Democratic Republic of the", Code = "cod", Flag = "cd.png" },
            new Country() { Label = "Cook Islands", Code = "cok", Flag = "ck.png" },
            new Country() { Label = "Costa Rica", Code = "cri", Flag = "cr.png" },
            new Country() { Label = "Cote d'Ivoire", Code = "civ", Flag = "ci.png" },
            new Country() { Label = "Croatia", Code = "hrv", Flag = "hr.png" },
            new Country() { Label = "Cuba", Code = "cub", Flag = "cu.png" },
            new Country() { Label = "Curacao", Code = "cuw", Flag = "cw.png" },
            new Country() { Label = "Cyprus", Code = "cyp", Flag = "cy.png" },
            new Country() { Label = "Czech Republic", Code = "cze", Flag = "cz.png" },
            new Country() { Label = "Denmark", Code = "dnk", Flag = "dk.png" },
            new Country() { Label = "Djibouti", Code = "dji", Flag = "dj.png" },
            new Country() { Label = "Dominica", Code = "dma", Flag = "dm.png" },
            new Country() { Label = "Dominican Republic", Code = "dom", Flag = "do.png" },
            new Country() { Label = "Ecuador", Code = "ecu", Flag = "ec.png" },
            new Country() { Label = "Egypt", Code = "egy", Flag = "eg.png" },
            new Country() { Label = "El Salvador", Code = "slv", Flag = "sv.png" },
            new Country() { Label = "Equatorial Guinea", Code = "gnq", Flag = "gq.png" },
            new Country() { Label = "Eritrea", Code = "eri", Flag = "er.png" },
            new Country() { Label = "Estonia", Code = "est", Flag = "ee.png" },
            new Country() { Label = "Ethiopia", Code = "eth", Flag = "et.png" },
            new Country() { Label = "Falkland Islands (Malvinas)", Code = "flk", Flag = "fk.png" },
            new Country() { Label = "Faroe Islands", Code = "fro", Flag = "fo.png" },
            new Country() { Label = "Fiji", Code = "fji", Flag = "fj.png" },
            new Country() { Label = "Finland", Code = "fin", Flag = "fi.png" },
            new Country() { Label = "France", Code = "fra", Flag = "fr.png" },
            new Country() { Label = "French Guiana", Code = "guf", Flag = "gf.png" },
            new Country() { Label = "French Polynesia", Code = "pyf", Flag = "pf.png" },
            new Country() { Label = "French Southern Territories", Code = "fst", Flag = "tf.png" },
            new Country() { Label = "Gabon", Code = "gab", Flag = "ga.png" },
            new Country() { Label = "Gambia", Code = "gmb", Flag = "gm.png" },
            new Country() { Label = "Georgia", Code = "geo", Flag = "ge.png" },
            new Country() { Label = "Germany", Code = "deu", Flag = "de.png" },
            new Country() { Label = "Ghana", Code = "gha", Flag = "gh.png" },
            new Country() { Label = "Gibraltar", Code = "gib", Flag = "gi.png" },
            new Country() { Label = "Greece", Code = "grc", Flag = "gr.png" },
            new Country() { Label = "Greenland", Code = "grl", Flag = "gl.png" },
            new Country() { Label = "Grenada", Code = "grd", Flag = "gd.png" },
            new Country() { Label = "Guadeloupe", Code = "glp", Flag = "gp.png" },
            new Country() { Label = "Guam", Code = "gum", Flag = "gu.png" },
            new Country() { Label = "Guatemala", Code = "gtm", Flag = "gt.png" },
            new Country() { Label = "Guernsey", Code = "ggy", Flag = "gg.png" },
            new Country() { Label = "Guinea", Code = "gin", Flag = "gn.png" },
            new Country() { Label = "Guinea-Bissau", Code = "gnb", Flag = "gw.png" },
            new Country() { Label = "Guyana", Code = "guy", Flag = "gy.png" },
            new Country() { Label = "Haiti", Code = "hti", Flag = "ht.png" },
            new Country() { Label = "Heard Island and McDonald Islands", Code = "him", Flag = "hm.png" },
            new Country() { Label = "Holy See (Vatican City State)", Code = "vat", Flag = "va.png" },
            new Country() { Label = "Honduras", Code = "hnd", Flag = "hn.png" },
            new Country() { Label = "Hong Kong", Code = "hkg", Flag = "hk.png" },
            new Country() { Label = "Hungary", Code = "hun", Flag = "hu.png" },
            new Country() { Label = "Iceland", Code = "isl", Flag = "is.png" },
            new Country() { Label = "India", Code = "ind", Flag = "in.png" },
            new Country() { Label = "Indonesia", Code = "idn", Flag = "id.png" },
            new Country() { Label = "Iran, Islamic Republic of", Code = "irn", Flag = "ir.png" },
            new Country() { Label = "Iraq", Code = "irq", Flag = "iq.png" },
            new Country() { Label = "Ireland", Code = "irl", Flag = "ie.png" },
            new Country() { Label = "Isle of Man", Code = "imn", Flag = "im.png" },
            new Country() { Label = "Israel", Code = "isr", Flag = "il.png" },
            new Country() { Label = "Italy", Code = "ita", Flag = "it.png" },
            new Country() { Label = "Jamaica", Code = "jam", Flag = "jm.png" },
            new Country() { Label = "Japan", Code = "jpn", Flag = "jp.png" },
            new Country() { Label = "Jersey", Code = "jey", Flag = "je.png" },
            new Country() { Label = "Jordan", Code = "jor", Flag = "jo.png" },
            new Country() { Label = "Kazakhstan", Code = "kaz", Flag = "kz.png" },
            new Country() { Label = "Kenya", Code = "ken", Flag = "ke.png" },
            new Country() { Label = "Kiribati", Code = "kir", Flag = "ki.png" },
            new Country() { Label = "Korea, Democratic People's Republic of", Code = "prk", Flag = "kp.png" },
            new Country() { Label = "Korea, Republic of", Code = "kor", Flag = "kr.png" },
            new Country() { Label = "Kuwait", Code = "kwt", Flag = "kw.png" },
            new Country() { Label = "Kyrgyzstan", Code = "kgz", Flag = "kg.png" },
            new Country() { Label = "Lao People's Democratic Republic", Code = "lao", Flag = "la.png" },
            new Country() { Label = "Latvia", Code = "lva", Flag = "lv.png" },
            new Country() { Label = "Lebanon", Code = "lbn", Flag = "lb.png" },
            new Country() { Label = "Lesotho", Code = "lso", Flag = "ls.png" },
            new Country() { Label = "Liberia", Code = "lbr", Flag = "lr.png" },
            new Country() { Label = "Libyan Arab Jamahiriya", Code = "lby", Flag = "ly.png" },
            new Country() { Label = "Liechtenstein", Code = "lie", Flag = "li.png" },
            new Country() { Label = "Lithuania", Code = "ltu", Flag = "lt.png" },
            new Country() { Label = "Luxembourg", Code = "lux", Flag = "lu.png" },
            new Country() { Label = "Macao", Code = "mac", Flag = "mo.png" },
            new Country() { Label = "Macedonia, The former Yugoslav Republic of", Code = "mkd", Flag = "mk.png" },
            new Country() { Label = "Madagascar", Code = "mdg", Flag = "mg.png" },
            new Country() { Label = "Malawi", Code = "mwi", Flag = "mw.png" },
            new Country() { Label = "Malaysia", Code = "mys", Flag = "my.png" },
            new Country() { Label = "Maldives", Code = "mdv", Flag = "mv.png" },
            new Country() { Label = "Mali", Code = "mli", Flag = "ml.png" },
            new Country() { Label = "Malta", Code = "mlt", Flag = "mt.png" },
            new Country() { Label = "Marshall Islands", Code = "mhl", Flag = "mh.png" },
            new Country() { Label = "Martinique", Code = "mtq", Flag = "mq.png" },
            new Country() { Label = "Mauritania", Code = "mrt", Flag = "mr.png" },
            new Country() { Label = "Mauritius", Code = "mus", Flag = "mu.png" },
            new Country() { Label = "Mayotte", Code = "myt", Flag = "yt.png" },
            new Country() { Label = "Mexico", Code = "mex", Flag = "mx.png" },
            new Country() { Label = "Micronesia, Federated States of", Code = "fsm", Flag = "fm.png" },
            new Country() { Label = "Moldova, Republic of", Code = "mda", Flag = "md.png" },
            new Country() { Label = "Monaco", Code = "mco", Flag = "mc.png" },
            new Country() { Label = "Mongolia", Code = "mng", Flag = "mn.png" },
            new Country() { Label = "Montenegro", Code = "mne", Flag = "me.png" },
            new Country() { Label = "Montserrat", Code = "msr", Flag = "ms.png" },
            new Country() { Label = "Morocco", Code = "mar", Flag = "ma.png" },
            new Country() { Label = "Mozambique", Code = "moz", Flag = "mz.png" },
            new Country() { Label = "Myanmar", Code = "mmr", Flag = "mm.png" },
            new Country() { Label = "Namibia", Code = "nam", Flag = "na.png" },
            new Country() { Label = "Nauru", Code = "nru", Flag = "nr.png" },
            new Country() { Label = "Nepal", Code = "npl", Flag = "np.png" },
            new Country() { Label = "Netherlands", Code = "nld", Flag = "nl.png" },
            new Country() { Label = "New Caledonia", Code = "ncl", Flag = "nc.png" },
            new Country() { Label = "New Zealand", Code = "nzl", Flag = "nz.png" },
            new Country() { Label = "Nicaragua", Code = "nic", Flag = "ni.png" },
            new Country() { Label = "Niger", Code = "ner", Flag = "ne.png" },
            new Country() { Label = "Nigeria", Code = "nga", Flag = "ng.png" },
            new Country() { Label = "Niue", Code = "niu", Flag = "nu.png" },
            new Country() { Label = "Norfolk Island", Code = "nfk", Flag = "nf.png" },
            new Country() { Label = "Northern Mariana Islands", Code = "mnp", Flag = "mp.png" },
            new Country() { Label = "Norway", Code = "nor", Flag = "no.png" },
            new Country() { Label = "Oman", Code = "omn", Flag = "om.png" },
            new Country() { Label = "Pakistan", Code = "pak", Flag = "pk.png" },
            new Country() { Label = "Palau", Code = "plw", Flag = "pw.png" },
            new Country() { Label = "Palestinian Territory, Occupied", Code = "pse", Flag = "ps.png" },
            new Country() { Label = "Panama", Code = "pan", Flag = "pa.png" },
            new Country() { Label = "Papua New Guinea", Code = "png", Flag = "pg.png" },
            new Country() { Label = "Paraguay", Code = "pry", Flag = "py.png" },
            new Country() { Label = "Peru", Code = "per", Flag = "pe.png" },
            new Country() { Label = "Philippines", Code = "phl", Flag = "ph.png" },
            new Country() { Label = "Pitcairn", Code = "pcn", Flag = "pn.png" },
            new Country() { Label = "Poland", Code = "pol", Flag = "pl.png" },
            new Country() { Label = "Portugal", Code = "prt", Flag = "pt.png" },
            new Country() { Label = "Puerto Rico", Code = "pri", Flag = "pr.png" },
            new Country() { Label = "Qatar", Code = "qat", Flag = "qa.png" },
            new Country() { Label = "Reunion", Code = "reu", Flag = "re.png" },
            new Country() { Label = "Romania", Code = "rou", Flag = "ro.png" },
            new Country() { Label = "Russian Federation", Code = "rus", Flag = "ru.png" },
            new Country() { Label = "Rwanda", Code = "rwa", Flag = "rw.png" },
            new Country() { Label = "Saint Barthelemy", Code = "blm", Flag = "bl.png" },
            new Country() { Label = "Saint Helena, Ascension and Tristan Da Cunha", Code = "shn", Flag = "sh.png" },
            new Country() { Label = "Saint Kitts and Nevis", Code = "kna", Flag = "kn.png" },
            new Country() { Label = "Saint Lucia", Code = "lca", Flag = "lc.png" },
            new Country() { Label = "Saint Martin (French Part)", Code = "maf", Flag = "mf.png" },
            new Country() { Label = "Saint Pierre and Miquelon", Code = "spm", Flag = "pm.png" },
            new Country() { Label = "Saint Vincent and The Grenadines", Code = "vct", Flag = "vc.png" },
            new Country() { Label = "Samoa", Code = "wsm", Flag = "ws.png" },
            new Country() { Label = "San Marino", Code = "smr", Flag = "sm.png" },
            new Country() { Label = "Sao Tome and Principe", Code = "stp", Flag = "st.png" },
            new Country() { Label = "Saudi Arabia", Code = "sau", Flag = "sa.png" },
            new Country() { Label = "Senegal", Code = "sen", Flag = "sn.png" },
            new Country() { Label = "Serbia", Code = "srb", Flag = "rs.png" },
            new Country() { Label = "Seychelles", Code = "syc", Flag = "sc.png" },
            new Country() { Label = "Sierra Leone", Code = "sle", Flag = "sl.png" },
            new Country() { Label = "Singapore", Code = "sgp", Flag = "sg.png" },
            new Country() { Label = "Sint Maarten (Dutch Part)", Code = "sxm", Flag = "sx.png" },
            new Country() { Label = "Slovakia", Code = "svk", Flag = "sk.png" },
            new Country() { Label = "Slovenia", Code = "svn", Flag = "si.png" },
            new Country() { Label = "Solomon Islands", Code = "slb", Flag = "sb.png" },
            new Country() { Label = "Somalia", Code = "som", Flag = "so.png" },
            new Country() { Label = "South Africa", Code = "zaf", Flag = "za.png" },
            new Country() { Label = "South Georgia and The South Sandwich Islands", Code = "sgt", Flag = "gs.png" },
            new Country() { Label = "South Sudan", Code = "ssd", Flag = "ss.png" },
            new Country() { Label = "Spain", Code = "esp", Flag = "es.png" },
            new Country() { Label = "Sri Lanka", Code = "lka", Flag = "lk.png" },
            new Country() { Label = "Sudan", Code = "sdn", Flag = "sd.png" },
            new Country() { Label = "Suriname", Code = "sur", Flag = "sr.png" },
            new Country() { Label = "Svalbard and Jan Mayen", Code = "sjm", Flag = "sj.png" },
            new Country() { Label = "Swaziland", Code = "swz", Flag = "sz.png" },
            new Country() { Label = "Sweden", Code = "swe", Flag = "se.png" },
            new Country() { Label = "Switzerland", Code = "che", Flag = "ch.png" },
            new Country() { Label = "Syrian Arab Republic", Code = "syr", Flag = "sy.png" },
            new Country() { Label = "Taiwan, Province of China", Code = "tpc", Flag = "tw.png" },
            new Country() { Label = "Tajikistan", Code = "tjk", Flag = "tj.png" },
            new Country() { Label = "Tanzania, United Republic of", Code = "tza", Flag = "tz.png" },
            new Country() { Label = "Thailand", Code = "tha", Flag = "th.png" },
            new Country() { Label = "Timor-Leste", Code = "tls", Flag = "tl.png" },
            new Country() { Label = "Togo", Code = "tgo", Flag = "tg.png" },
            new Country() { Label = "Tokelau", Code = "tkl", Flag = "tk.png" },
            new Country() { Label = "Tonga", Code = "ton", Flag = "to.png" },
            new Country() { Label = "Trinidad and Tobago", Code = "tto", Flag = "tt.png" },
            new Country() { Label = "Tunisia", Code = "tun", Flag = "tn.png" },
            new Country() { Label = "Turkey", Code = "tur", Flag = "tr.png" },
            new Country() { Label = "Turkmenistan", Code = "tkm", Flag = "tm.png" },
            new Country() { Label = "Turks and Caicos Islands", Code = "tca", Flag = "tc.png" },
            new Country() { Label = "Tuvalu", Code = "tuv", Flag = "tv.png" },
            new Country() { Label = "Uganda", Code = "uga", Flag = "ug.png" },
            new Country() { Label = "Ukraine", Code = "ukr", Flag = "ua.png" },
            new Country() { Label = "United Arab Emirates", Code = "are", Flag = "ae.png" },
            new Country() { Label = "United Kingdom", Code = "gbr", Flag = "gb.png" },
            new Country() { Label = "United States", Code = "usa", Flag = "us.png" },
            new Country() { Label = "United States Minor Outlying Islands", Code = "usm", Flag = "um.png" },
            new Country() { Label = "Uruguay", Code = "ury", Flag = "uy.png" },
            new Country() { Label = "Uzbekistan", Code = "uzb", Flag = "uz.png" },
            new Country() { Label = "Vanuatu", Code = "vut", Flag = "vu.png" },
            new Country() { Label = "Venezuela, Bolivarian Republic of", Code = "ven", Flag = "ve.png" },
            new Country() { Label = "Viet Nam", Code = "vnm", Flag = "vn.png" },
            new Country() { Label = "Virgin Islands, British", Code = "vgb", Flag = "vg.png" },
            new Country() { Label = "Virgin Islands, U.S.", Code = "vir", Flag = "vi.png" },
            new Country() { Label = "Wallis and Futuna", Code = "wlf", Flag = "wf.png" },
            new Country() { Label = "Western Sahara", Code = "esh", Flag = "eh.png" },
            new Country() { Label = "Yemen", Code = "yem", Flag = "ye.png" },
            new Country() { Label = "Zambia", Code = "zmb", Flag = "zm.png" },
            new Country() { Label = "Zimbabwe", Code = "zwe", Flag = "zw.png" },
            };

            return result;
        }
    }
}