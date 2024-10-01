using App.Core.Enums;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Data.Sql.Core;

public static class FillBaseDictionaries {
    #region Countries

    public const string InsertCountries = @"
insert into ""Countries"" (""Id"", ""Name"", ""Code"", ""Iso"") values

 (1, 'Afghanistan', 'AF', 'AFG')
,(2, 'Aland Islands', 'AX', 'ALA')
,(3, 'Albania', 'AL', 'ALB')
,(4, 'Algeria', 'DZ', 'DZA')
,(5, 'American Samoa', 'AS', 'ASM')
,(6, 'Andorra', 'AD', 'AND')
,(7, 'Angola', 'AO', 'AGO')
,(8, 'Anguilla', 'AI', 'AIA')
,(9, 'Antarctica', 'AQ', 'ATA')
,(10, 'Antigua and Barbuda', 'AG', 'ATG')
,(11, 'Argentina', 'AR', 'ARG')
,(12, 'Armenia', 'AM', 'ARM')
,(13, 'Aruba', 'AW', 'ABW')
,(14, 'Australia', 'AU', 'AUS')
,(15, 'Austria', 'AT', 'AUT')
,(16, 'Azerbaijan', 'AZ', 'AZE')
,(17, 'Bahamas', 'BS', 'BHS')
,(18, 'Bahrain', 'BH', 'BHR')
,(19, 'Bangladesh', 'BD', 'BGD')
,(20, 'Barbados', 'BB', 'BRB')
,(21, 'Belarus', 'BY', 'BLR')
,(22, 'Belgium', 'BE', 'BEL')
,(23, 'Belize', 'BZ', 'BLZ')
,(24, 'Benin', 'BJ', 'BEN')
,(25, 'Bermuda', 'BM', 'BMU')
,(26, 'Bhutan', 'BT', 'BTN')
,(27, 'Bolivia', 'BO', 'BOL')
,(28, 'Bonaire', 'BQ', 'BES')
,(29, 'Bosnia and Herzegovina', 'BA', 'BIH')
,(30, 'Botswana', 'BW', 'BWA')
,(31, 'Bouvet Island', 'BV', 'BVT')
,(32, 'Brazil', 'BR', 'BRA')
,(33, 'British Indian Ocean Territory', 'IO', 'IOT')
,(34, 'Brunei Darussalam', 'BN', 'BRN')
,(35, 'Bulgaria', 'BG', 'BGR')
,(36, 'Burkina Faso', 'BF', 'BFA')
,(37, 'Burundi', 'BI', 'BDI')
,(38, 'Cambodia', 'KH', 'KHM')
,(39, 'Cameroon', 'CM', 'CMR')
,(40, 'Canada', 'CA', 'CAN')
,(41, 'Cape Verde', 'CV', 'CPV')
,(42, 'Cayman Islands', 'KY', 'CYM')
,(43, 'Central African Reprivate', 'CF', 'CAF')
,(44, 'Chad', 'TD', 'TCD')
,(45, 'Chile', 'CL', 'CHL')
,(46, 'China', 'CN', 'CHN')
,(47, 'Christmas Island', 'CX', 'CXR')
,(48, 'Cocos (Keeling) Islands', 'CC', 'CCK')
,(49, 'Colombia', 'CO', 'COL')
,(50, 'Comoros', 'KM', 'COM')
,(51, 'Congo', 'CG', 'COG')
,(53, 'Cook Islands', 'CK', 'COK')
,(54, 'Costa Rica', 'CR', 'CRI')
,(55, 'Cote d''Ivoire', 'CI', 'CIV')
,(56, 'Croatia', 'HR', 'HRV')
,(57, 'Cuba', 'CU', 'CUB')
,(58, 'Curacao', 'CW', 'CUW')
,(59, 'Cyprus', 'CY', 'CYP')
,(60, 'Czech Reprivate', 'CZ', 'CZE')
,(61, 'Denmark', 'DK', 'DNK')
,(62, 'Djibouti', 'DJ', 'DJI')
,(63, 'Dominica', 'DM', 'DMA')
,(64, 'Dominican Reprivate', 'DO', 'DOM')
,(65, 'Ecuador', 'EC', 'ECU')
,(66, 'Egypt', 'EG', 'EGY')
,(67, 'El Salvador', 'SV', 'SLV')
,(68, 'Equatorial Guinea', 'GQ', 'GNQ')
,(69, 'Eritrea', 'ER', 'ERI')
,(70, 'Estonia', 'EE', 'EST')
,(71, 'Ethiopia', 'ET', 'ETH')
,(72, 'Falkland Islands (Malvinas)', 'FK', 'FLK')
,(73, 'Faroe Islands', 'FO', 'FRO')
,(74, 'Fiji', 'FJ', 'FJI')
,(75, 'Finland', 'FI', 'FIN')
,(76, 'France', 'FR', 'FRA')
,(77, 'French Guiana', 'GF', 'GUF')
,(78, 'French Polynesia', 'PF', 'PYF')
,(79, 'French Southern Territories', 'TF', 'ATF')
,(80, 'Gabon', 'GA', 'GAB')
,(81, 'Gambia', 'GM', 'GMB')
,(82, 'Georgia', 'GE', 'GEO')
,(83, 'Germany', 'DE', 'DEU')
,(84, 'Ghana', 'GH', 'GHA')
,(85, 'Gibraltar', 'GI', 'GIB')
,(86, 'Greece', 'GR', 'GRC')
,(87, 'Greenland', 'GL', 'GRL')
,(88, 'Grenada', 'GD', 'GRD')
,(89, 'Guadeloupe', 'GP', 'GLP')
,(90, 'Guam', 'GU', 'GUM')
,(91, 'Guatemala', 'GT', 'GTM')
,(92, 'Guernsey', 'GG', 'GGY')
,(93, 'Guinea', 'GN', 'GIN')
,(94, 'Guinea-Bissau', 'GW', 'GNB')
,(95, 'Guyana', 'GY', 'GUY')
,(96, 'Haiti', 'HT', 'HTI')
,(97, 'Heard Island and McDonald Islands', 'HM', 'HMD')
,(98, 'Holy See (Vatican City State)', 'VA', 'VAT')
,(99, 'Honduras', 'HN', 'HND')
,(100, 'Hong Kong', 'HK', 'HKG')
,(101, 'Hungary', 'HU', 'HUN')
,(102, 'Iceland', 'IS', 'ISL')
,(103, 'India', 'IN', 'IND')
,(104, 'Indonesia', 'ID', 'IDN')
,(105, 'Iran, Islamic Reprivate of', 'IR', 'IRN')
,(106, 'Iraq', 'IQ', 'IRQ')
,(107, 'Ireland', 'IE', 'IRL')
,(108, 'Isle of Man', 'IM', 'IMN')
,(109, 'Israel', 'IL', 'ISR')
,(110, 'Italy', 'IT', 'ITA')
,(111, 'Jamaica', 'JM', 'JAM')
,(112, 'Japan', 'JP', 'JPN')
,(113, 'Jersey', 'JE', 'JEY')
,(114, 'Jordan', 'JO', 'JOR')
,(115, 'Kazakhstan', 'KZ', 'KAZ')
,(116, 'Kenya', 'KE', 'KEN')
,(117, 'Kiribati', 'KI', 'KIR')
,(118, 'North Korea', 'KP', 'PRK')
,(119, 'South Korea', 'KR', 'KOR')
,(120, 'Kuwait', 'KW', 'KWT')
,(121, 'Kyrgyzstan', 'KG', 'KGZ')
,(122, 'Lao', 'LA', 'LAO')
,(123, 'Latvia', 'LV', 'LVA')
,(124, 'Lebanon', 'LB', 'LBN')
,(125, 'Lesotho', 'LS', 'LSO')
,(126, 'Liberia', 'LR', 'LBR')
,(127, 'Libyan Arab Jamahiriya', 'LY', 'LBY')
,(128, 'Liechtenstein', 'LI', 'LIE')
,(129, 'Lithuania', 'LT', 'LTU')
,(130, 'Luxembourg', 'LU', 'LUX')
,(131, 'Macao', 'MO', 'MAC')
,(132, 'Macedonia', 'MK', 'MKD')
,(133, 'Madagascar', 'MG', 'MDG')
,(134, 'Malawi', 'MW', 'MWI')
,(135, 'Malaysia', 'MY', 'MYS')
,(136, 'Maldives', 'MV', 'MDV')
,(137, 'Mali', 'ML', 'MLI')
,(138, 'Malta', 'MT', 'MLT')
,(139, 'Marshall Islands', 'MH', 'MHL')
,(140, 'Martinique', 'MQ', 'MTQ')
,(141, 'Mauritania', 'MR', 'MRT')
,(142, 'Mauritius', 'MU', 'MUS')
,(143, 'Mayotte', 'YT', 'MYT')
,(144, 'Mexico', 'MX', 'MEX')
,(145, 'Micronesia', 'FM', 'FSM')
,(146, 'Moldova, Reprivate of', 'MD', 'MDA')
,(147, 'Monaco', 'MC', 'MCO')
,(148, 'Mongolia', 'MN', 'MNG')
,(149, 'Montenegro', 'ME', 'MNE')
,(150, 'Montserrat', 'MS', 'MSR')
,(151, 'Morocco', 'MA', 'MAR')
,(152, 'Mozambique', 'MZ', 'MOZ')
,(153, 'Myanmar', 'MM', 'MMR')
,(154, 'Namibia', 'NA', 'NAM')
,(155, 'Nauru', 'NR', 'NRU')
,(156, 'Nepal', 'NP', 'NPL')
,(157, 'Netherlands', 'NL', 'NLD')
,(158, 'New Caledonia', 'NC', 'NCL')
,(159, 'New Zealand', 'NZ', 'NZL')
,(160, 'Nicaragua', 'NI', 'NIC')
,(161, 'Niger', 'NE', 'NER')
,(162, 'Nigeria', 'NG', 'NGA')
,(163, 'Niue', 'NU', 'NIU')
,(164, 'Norfolk Island', 'NF', 'NFK')
,(165, 'Northern Mariana Islands', 'MP', 'MNP')
,(166, 'Norway', 'NO', 'NOR')
,(167, 'Oman', 'OM', 'OMN')
,(168, 'Pakistan', 'PK', 'PAK')
,(169, 'Palau', 'PW', 'PLW')
,(170, 'Palestinian Territory', 'PS', 'PSE')
,(171, 'Panama', 'PA', 'PAN')
,(172, 'Papua New Guinea', 'PG', 'PNG')
,(173, 'Paraguay', 'PY', 'PRY')
,(174, 'Peru', 'PE', 'PER')
,(175, 'Philippines', 'PH', 'PHL')
,(176, 'Pitcairn', 'PN', 'PCN')
,(177, 'Poland', 'PL', 'POL')
,(178, 'Portugal', 'PT', 'PRT')
,(179, 'Puerto Rico', 'PR', 'PRI')
,(180, 'Qatar', 'QA', 'QAT')
,(181, 'Reunion', 'RE', 'REU')
,(182, 'Romania', 'RO', 'ROU')
,(183, 'Russian Federation', 'RU', 'RUS')
,(184, 'Rwanda', 'RW', 'RWA')
,(185, 'Saint Barthelemy', 'BL', 'BLM')
,(186, 'Saint Helena', 'SH', 'SHN')
,(187, 'Saint Kitts and Nevis', 'KN', 'KNA')
,(188, 'Saint Lucia', 'LC', 'LCA')
,(189, 'Saint Martin (French Part)', 'MF', 'MAF')
,(190, 'Saint Pierre and Miquelon', 'PM', 'SPM')
,(191, 'Saint Vincent and The Grenadines', 'VC', 'VCT')
,(192, 'Samoa', 'WS', 'WSM')
,(193, 'San Marino', 'SM', 'SMR')
,(194, 'Sao Tome and Principe', 'ST', 'STP')
,(195, 'Saudi Arabia', 'SA', 'SAU')
,(196, 'Senegal', 'SN', 'SEN')
,(197, 'Serbia', 'RS', 'SRB')
,(198, 'Seychelles', 'SC', 'SYC')
,(199, 'Sierra Leone', 'SL', 'SLE')
,(200, 'Singapore', 'SG', 'SGP')
,(201, 'Sint Maarten (Dutch Part)', 'SX', 'SXM')
,(202, 'Slovakia', 'SK', 'SVK')
,(203, 'Slovenia', 'SI', 'SVN')
,(204, 'Solomon Islands', 'SB', 'SLB')
,(205, 'Somalia', 'SO', 'SOM')
,(206, 'South Africa', 'ZA', 'ZAF')
,(207, 'South Georgia', 'GS', 'SGS')
,(208, 'Spain', 'ES', 'ESP')
,(209, 'Sri Lanka', 'LK', 'LKA')
,(210, 'Sudan', 'SD', 'SDN')
,(211, 'Suriname', 'SR', 'SUR')
,(212, 'Svalbard and Jan Mayen', 'SJ', 'SJM')
,(213, 'Swaziland', 'SZ', 'SWZ')
,(214, 'Sweden', 'SE', 'SWE')
,(215, 'Switzerland', 'CH', 'CHE')
,(216, 'Syrian Arab Reprivate', 'SY', 'SYR')
,(217, 'Taiwan', 'TW', 'TWN')
,(218, 'Tajikistan', 'TJ', 'TJK')
,(219, 'Tanzania, United Reprivate of', 'TZ', 'TZA')
,(220, 'Thailand', 'TH', 'THA')
,(221, 'Timor-Leste', 'TL', 'TLS')
,(222, 'Togo', 'TG', 'TGO')
,(223, 'Tokelau', 'TK', 'TKL')
,(224, 'Tonga', 'TO', 'TON')
,(225, 'Trinidad and Tobago', 'TT', 'TTO')
,(226, 'Tunisia', 'TN', 'TUN')
,(227, 'Turkey', 'TR', 'TUR')
,(228, 'Turkmenistan', 'TM', 'TKM')
,(229, 'Turks and Caicos Islands', 'TC', 'TCA')
,(230, 'Tuvalu', 'TV', 'TUV')
,(231, 'Uganda', 'UG', 'UGA')
,(232, 'Ukraine', 'UA', 'UKR')
,(233, 'United Arab Emirates', 'AE', 'ARE')
,(234, 'United Kingdom', 'GB', 'GBR')
,(235, 'United States', 'US', 'USA')
,(236, 'United States Minor Outlying Islands', 'UM', 'UMI')
,(237, 'Uruguay', 'UY', 'URY')
,(238, 'Uzbekistan', 'UZ', 'UZB')
,(239, 'Vanuatu', 'VU', 'VUT')
,(241, 'Venezuela, Bolivarian Reprivate of', 'VE', 'VEN')
,(242, 'Vietnam', 'VN', 'VNM')
,(243, 'Virgin Islands, British', 'VG', 'VGB')
,(244, 'Virgin Islands, U.S.', 'VI', 'VIR')
,(245, 'Wallis and Futuna', 'WF', 'WLF')
,(246, 'Western Sahara', 'EH', 'ESH')
,(247, 'Yemen', 'YE', 'YEM')
,(248, 'Zambia', 'ZM', 'ZMB')
,(249, 'Zimbabwe', 'ZW', 'ZWE')
,(250, 'Netherlands Antilles', 'AN', 'ANT')
,(251, 'Federated States Of Micronesia', 'FM', 'FSM')
,(252, 'Marshall Islands', 'MH', 'MHL')
,(253, 'Palau', 'PW', 'PLW')
,(254, 'South Sudan', 'SS', 'SSD')
,(255, 'Kosovo', 'XK', 'XKX')";

    #endregion

    #region CountryStates

    public const string InsertCountryStates = @"
insert into ""CountryStates"" (""Id"", ""Name"", ""Code"", ""CountryId"") values
(1, 'Alabama', 'AL', 235),
(2, 'Alaska', 'AK', 235),
(3, 'Arizona', 'AZ', 235),
(4, 'Arkansas', 'AR', 235),
(5, 'California', 'CA', 235),
(6, 'Colorado', 'CO', 235),
(7, 'Connecticut', 'CT', 235),
(8, 'Delaware', 'DE', 235),
(9, 'Florida', 'FL', 235),
(10, 'Georgia', 'GA', 235),
(11, 'Hawaii', 'HI', 235),
(12, 'Idaho', 'ID', 235),
(13, 'Illinois', 'IL', 235),
(14, 'Indiana', 'IN', 235),
(15, 'Iowa', 'IA', 235),
(16, 'Kansas', 'KS', 235),
(17, 'Kentucky', 'KY', 235),
(18, 'Louisiana', 'LA', 235),
(19, 'Maine', 'ME', 235),
(20, 'Maryland', 'MD', 235),
(21, 'Massachusetts', 'MA', 235),
(22, 'Michigan', 'MI', 235),
(23, 'Minnesota', 'MN', 235),
(24, 'Mississippi', 'MS', 235),
(25, 'Missouri', 'MO', 235),
(26, 'Montana', 'MT', 235),
(27, 'Nebraska', 'NE', 235),
(28, 'Nevada', 'NV', 235),
(29, 'New Hampshire', 'NH', 235),
(30, 'New Jersey', 'NJ', 235),
(31, 'New Mexico', 'NM', 235),
(32, 'New York', 'NY', 235),
(33, 'North Carolina', 'NC', 235),
(34, 'North Dakota', 'ND', 235),
(35, 'Ohio', 'OH', 235),
(36, 'Oklahoma', 'OK', 235),
(37, 'Oregon', 'OR', 235),
(38, 'Pennsylvania', 'PA', 235),
(39, 'Rhode Island', 'RI', 235),
(40, 'South Carolina', 'SC', 235),
(41, 'South Dakota', 'SD', 235),
(42, 'Tennessee', 'TN', 235),
(43, 'Texas', 'TX', 235),
(44, 'Utah', 'UT', 235),
(45, 'Vermont', 'VT', 235),
(46, 'Virginia', 'VA', 235),
(47, 'Washington', 'WA', 235),
(48, 'West Virginia', 'WV', 235),
(49, 'Wisconsin', 'WI', 235),
(50, 'Wyoming', 'WY', 235),
(51, 'American Samoa', 'AS', 235),
(52, 'District of Columbia', 'DC', 235),
(53, 'Federated States of Micronesia', 'FM', 235),
(54, 'Guam', 'GU', 235),
(55, 'Marshall Islands', 'MH', 235),
(56, 'Northern Mariana Islands', 'MP', 235),
(57, 'Palau', 'PW', 235),
(58, 'Puerto Rico', 'PR', 235),
(59, 'Virgin Islands', 'VI', 235)";

    #endregion

    #region InsertLocales

    public const string InsertLocales = @"

INSERT INTO ""LocaleCodes""(""Id"", ""Name"", ""Code"")
VALUES
(	1	, 'Albanian (Albania)', 'sq_AL')
,(	2	, 'Albanian', 'sq')
,(	3	, 'Arabic (Algeria)', 'ar_DZ')
,(	4	, 'Arabic (Bahrain)', 'ar_BH')
,(	5	, 'Arabic (Egypt)', 'ar_EG')
,(	6	, 'Arabic (Iraq)', 'ar_IQ')
,(	7	, 'Arabic (Jordan)', 'ar_JO')
,(	8	, 'Arabic (Kuwait)', 'ar_KW')
,(	9	, 'Arabic (Lebanon)', 'ar_LB')
,(	10	, 'Arabic (Libya)', 'ar_LY')
,(	11	, 'Arabic (Morocco)', 'ar_MA')
,(	12	, 'Arabic (Oman)', 'ar_OM')
,(	13	, 'Arabic (Qatar)', 'ar_QA')
,(	14	, 'Arabic (Saudi Arabia)', 'ar_SA')
,(	15	, 'Arabic (Sudan)', 'ar_SD')
,(	16	, 'Arabic (Syria)', 'ar_SY')
,(	17	, 'Arabic (Tunisia)', 'ar_TN')
,(	18	, 'Arabic (United Arab Emirates)', 'ar_AE')
,(	19	, 'Arabic (Yemen)', 'ar_YE')
,(	20	, 'Arabic', 'ar')
,(	21	, 'Belarusian (Belarus)', 'be_BY')
,(	22	, 'Belarusian', 'be')
,(	23	, 'Bulgarian (Bulgaria)', 'bg_BG')
,(	24	, 'Bulgarian', 'bg')
,(	25	, 'Catalan (Spain)', 'ca_ES')
,(	26	, 'Catalan', 'ca')
,(	27	, 'Chinese (China)', 'zh_CN')
,(	28	, 'Chinese (Hong Kong)', 'zh_HK')
,(	29	, 'Chinese (Singapore)', 'zh_SG')
,(	30	, 'Chinese (Taiwan)', 'zh_TW')
,(	31	, 'Chinese', 'zh')
,(	32	, 'Croatian (Croatia)', 'hr_HR')
,(	33	, 'Croatian', 'hr')
,(	34	, 'Czech (Czech Reprivate)', 'cs_CZ')
,(	35	, 'Czech', 'cs')
,(	36	, 'Danish (Denmark)', 'da_DK')
,(	37	, 'Danish', 'da')
,(	38	, 'Dutch (Belgium)', 'nl_BE')
,(	39	, 'Dutch (Netherlands)', 'nl_NL')
,(	40	, 'Dutch', 'nl')
,(	41	, 'English (Australia)', 'en_AU')
,(	42	, 'English (Canada)', 'en_CA')
,(	43	, 'English (India)', 'en_IN')
,(	44	, 'English (Ireland)', 'en_IE')
,(	45	, 'English (Malta)', 'en_MT')
,(	46	, 'English (New Zealand)', 'en_NZ')
,(	47	, 'English (Philippines)', 'en_PH')
,(	48	, 'English (Singapore)', 'en_SG')
,(	49	, 'English (South Africa)', 'en_ZA')
,(	50	, 'English (United Kingdom)', 'en_GB')
,(	51	, 'English (United States)', 'en_US')
,(	52	, 'English', 'en')
,(	53	, 'Estonian (Estonia)', 'et_EE')
,(	54	, 'Estonian', 'et')
,(	55	, 'Finnish (Finland)', 'fi_FI')
,(	56	, 'Finnish', 'fi')
,(	57	, 'French (Belgium)', 'fr_BE')
,(	58	, 'French (Canada)', 'fr_CA')
,(	59	, 'French (France)', 'fr_FR')
,(	60	, 'French (Luxembourg)', 'fr_LU')
,(	61	, 'French (Switzerland)', 'fr_CH')
,(	62	, 'French', 'fr')
,(	63	, 'German (Austria)', 'de_AT')
,(	64	, 'German (Germany)', 'de_DE')
,(	65	, 'German (Luxembourg)', 'de_LU')
,(	66	, 'German (Switzerland)', 'de_CH')
,(	67	, 'German', 'de')
,(	68	, 'Greek (Cyprus)', 'el_CY')
,(	69	, 'Greek (Greece)', 'el_GR')
,(	70	, 'Greek', 'el')
,(	71	, 'Hebrew (Israel)', 'iw_IL')
,(	72	, 'Hebrew', 'iw')
,(	73	, 'Hindi (India)', 'hi_IN')
,(	74	, 'Hungarian (Hungary)', 'hu_HU')
,(	75	, 'Hungarian', 'hu')
,(	76	, 'Icelandic (Iceland)', 'is_IS')
,(	77	, 'Icelandic', 'is')
,(	78	, 'Indonesian (Indonesia)', 'in_ID')
,(	79	, 'Indonesian', 'in')
,(	80	, 'Irish (Ireland)', 'ga_IE')
,(	81	, 'Irish', 'ga')
,(	82	, 'Italian (Italy)', 'it_IT')
,(	83	, 'Italian (Switzerland)', 'it_CH')
,(	84	, 'Italian', 'it')
,(	85	, 'Japanese (Japan)', 'ja_JP')
,(	86	, 'Japanese (Japan,JP)', 'ja_JP_JP')
,(	87	, 'Japanese', 'ja')
,(	88	, 'Korean (South Korea)', 'ko_KR')
,(	89	, 'Korean', 'ko')
,(	90	, 'Latvian (Latvia)', 'lv_LV')
,(	91	, 'Latvian', 'lv')
,(	92	, 'Lithuanian (Lithuania)', 'lt_LT')
,(	93	, 'Lithuanian', 'lt')
,(	94	, 'Macedonian (Macedonia)', 'mk_MK')
,(	95	, 'Macedonian', 'mk')
,(	96	, 'Malay (Malaysia)', 'ms_MY')
,(	97	, 'Malay', 'ms')
,(	98	, 'Maltese (Malta)', 'mt_MT')
,(	99	, 'Maltese', 'mt')
,(	100	, 'Norwegian (Norway)', 'no_NO')
,(	101	, 'Norwegian (Norway,Nynorsk)', 'no_NO_NY')
,(	102	, 'Norwegian', 'no')
,(	103	, 'Polish (Poland)', 'pl_PL')
,(	104	, 'Polish', 'pl')
,(	105	, 'Portuguese (Brazil)', 'pt_BR')
,(	106	, 'Portuguese (Portugal)', 'pt_PT')
,(	107	, 'Portuguese', 'pt')
,(	108	, 'Romanian (Romania)', 'ro_RO')
,(	109	, 'Romanian', 'ro')
,(	110	, 'Russian (Russia)', 'ru_RU')
,(	111	, 'Russian', 'ru')
,(	112	, 'Serbian (Bosnia and Herzegovina)', 'sr_BA')
,(	113	, 'Serbian (Montenegro)', 'sr_ME')
,(	114	, 'Serbian (Serbia and Montenegro)', 'sr_CS')
,(	115	, 'Serbian (Serbia)', 'sr_RS')
,(	116	, 'Serbian', 'sr')
,(	117	, 'Slovak (Slovakia)', 'sk_SK')
,(	118	, 'Slovak', 'sk')
,(	119	, 'Slovenian (Slovenia)', 'sl_SI')
,(	120	, 'Slovenian', 'sl')
,(	121	, 'Spanish (Argentina)', 'es_AR')
,(	122	, 'Spanish (Bolivia)', 'es_BO')
,(	123	, 'Spanish (Chile)', 'es_CL')
,(	124	, 'Spanish (Colombia)', 'es_CO')
,(	125	, 'Spanish (Costa Rica)', 'es_CR')
,(	126	, 'Spanish (Dominican Reprivate)', 'es_DO')
,(	127	, 'Spanish (Ecuador)', 'es_EC')
,(	128	, 'Spanish (El Salvador)', 'es_SV')
,(	129	, 'Spanish (Guatemala)', 'es_GT')
,(	130	, 'Spanish (Honduras)', 'es_HN')
,(	131	, 'Spanish (Mexico)', 'es_MX')
,(	132	, 'Spanish (Nicaragua)', 'es_NI')
,(	133	, 'Spanish (Panama)', 'es_PA')
,(	134	, 'Spanish (Paraguay)', 'es_PY')
,(	135	, 'Spanish (Peru)', 'es_PE')
,(	136	, 'Spanish (Puerto Rico)', 'es_PR')
,(	137	, 'Spanish (Spain)', 'es_ES')
,(	138	, 'Spanish (United States)', 'es_US')
,(	139	, 'Spanish (Uruguay)', 'es_UY')
,(	140	, 'Spanish (Venezuela)', 'es_VE')
,(	141	, 'Spanish', 'es')
,(	142	, 'Swedish (Sweden)', 'sv_SE')
,(	143	, 'Swedish', 'sv')
,(	144	, 'Thai (Thailand)', 'th_TH')
,(	145	, 'Thai (Thailand,TH)', 'th_TH_TH')
,(	146	, 'Thai', 'th')
,(	147	, 'Turkish (Turkey)', 'tr_TR')
,(	148	, 'Turkish', 'tr')
,(	149	, 'Ukrainian (Ukraine)', 'uk_UA')
,(	150	, 'Ukrainian', 'uk')
,(	151	, 'Vietnamese (Vietnam)', 'vi_VN')
,(	152	, 'Vietnamese', 'vi')

";

    #endregion

    #region InsertTaskActionTypes

    public const string InsertTaskActionTypes = @"

INSERT INTO ""TaskActionTypes""(""Id"", ""Name"", ""Code"")
VALUES
 (1, 'Created', 'Created')
,(2, 'Assigned', 'Assigned')
,(3, 'Updated', 'Updated')
";

    #endregion

    #region InsertTaskStateTypes

    public const string InsertTaskStateTypes = @"

INSERT INTO ""TaskStateTypes""(""Id"", ""Name"", ""Code"")
VALUES
 (1, 'Completed', 'Completed')
,(2, 'NotStarted', 'NotStarted')
,(3, 'InProgress', 'InProgress')
,(4, 'Deferred', 'Deferred')
";

    #endregion

    #region InsertApiEndpointTypes

    //    Base = 1,
    // WalletCreated = 2,
    // TaskActivated = 3,
    // TaskTakeRequest = 4
    public const string InsertApiEndpointTypes = @"
INSERT INTO ""ApiEndpointTypes""(""Id"", ""Name"", ""Code"")
VALUES
 (1, 'Base', 'Base')
,(2, 'Wallet Created', 'WalletCreated')
,(3, 'Task Activated', 'TaskActivated')
,(4, 'Task Take Request', 'TaskTakeRequest')";

    #endregion

    #region InsertTaskSubjectTypes

    private const string InsertTaskSubjectTypes = @"

INSERT INTO ""TaskSubjectTypes""(""Id"", ""Name"", ""Code"")
VALUES
 (1, 'Incoming', 'Incoming')
";

    #endregion

    #region InsertTaskTypes

    public const string InsertTaskTypes = @"

INSERT INTO ""TaskTypes""(""Id"", ""Name"", ""Code"")
VALUES
 (1, 'Base', 'Base')
,(2, 'First Registration', 'FirstRegistration')
";

    #endregion

    #region ObjectTypes

    public const string InsertObjectTypes = @"

INSERT INTO ""ObjectTypes""(""Id"", ""Name"", ""Code"")
VALUES
 (1, 'Task', 'Task') 
";

    public const string InsertObjectTypesPart2 = @"
INSERT INTO ""ObjectTypes""(""Id"", ""Name"", ""Code"") 
VALUES 
(2, 'AuditLog', 'AuditLog')
,(3, 'TaskExecutionNote', 'TaskExecutionNote')
,(4, 'Task Execution', 'TaskExecution')
,(5, 'User', 'User')
;
";

    #endregion

    #region InsertAuditEventTypes

    public const string InsertAuditEventTypes = @"
INSERT INTO ""AuditEventTypes""(""Id"", ""Name"", ""Code"")
VALUES
 (1, 'Task Created', 'TaskCreated')
,(2, 'Task Changed', 'TaskChanged')
,(10, 'Task Request Created', 'TaskRequestCreated')
,(11, 'Task Request Changed', 'TaskRequestChanged') 
";

    public const string InsertAuditEventTypesPart2 = @"
INSERT INTO ""AuditEventTypes""(""Id"", ""Name"", ""Code"")
VALUES
 (3, 'Task Assigned', 'TaskAssigned')
,(4, 'Task Assigned User Removed', 'TaskAssignedUserRemoved')
,(5, 'User Created', 'UserCreated')
,(12, 'Task Activated', 'TaskActivated')
,(13, 'User Blocked', 'UserBlocked')
,(22, 'User Changed', 'UserChanged')
";

    #endregion

    #region InsertFollowTypes

    public const string InsertFollowTypes = @"
INSERT INTO ""FollowTypes""(""Id"", ""Name"", ""Code"")
VALUES
 (1, 'Task', 'Task')
,(2, 'User', 'User')
,(3, 'Task request', 'TaskRequest')
,(4, 'Referral', 'Referral')
";

    #endregion

    #region TaskStateGroupTypes

    public const string InsertTaskStateGroupTypes = @"
INSERT INTO ""TaskStateGroupTypes""(""Id"", ""Name"", ""Code"")
VALUES
 (1, 'All', 'All')
,(2, 'Active', 'Active')
,(3, 'Closed', 'Closed')
";

    #endregion

    #region TelegramMessageTypes

    //    Task = 1,
    //External = 2
    public const string InsertTelegramMessageTypes = @"
INSERT INTO ""TelegramMessageTypes""(""Id"", ""Name"", ""Code"")
VALUES
 (1, 'Task', 'Task')
,(2, 'External', 'External')
";

    #endregion

    #region PaymentTypes

    public const string InsertPaymentTypes = @"
    INSERT INTO ""PaymentTypes""(""Id"", ""Name"", ""Code"")
    VALUES
     (1, 'Task Request', 'TaskRequest')
    ,(2, 'Referral Pair', 'ReferralPair')
    ";

    #endregion

    #region UserLogType

    public const string InsertUserLogTypes = @"
INSERT INTO ""UserLogType""(""Id"", ""Name"", ""Code"")
VALUES
 (1, 'Login', 'Login')
,(2, 'Referral Open', 'ReferralOpen')
,(3, 'Referral Error', 'ReferralError')
,(4, 'Referral Successful', 'ReferralSuccessful')
,(5, 'Referral Generated', 'ReferralGenerated')
";
    /*
     *     Login = 1,
        ReferralOpen = 2,
        ReferralError = 3,
        ReferralSuccessful = 4,
        ReferralGenerated = 5,
     */

    #endregion

    #region AddBaseAdminUser

    public const string InsertBaseAdminUser = @"
INSERT INTO ""Users""(
    ""Id"", ""CreatedOn"", ""UpdatedOn"", ""Email"", ""FirstName"", ""LastName"", ""ImageFileName"", ""IsActive"", ""Password"", ""Salt"", ""MFATypeId"", ""UserTypeId"", ""CountryId"", ""CountryStateId"")
    VALUES ('00000000-0000-0000-0000-000000000001', CURRENT_DATE, CURRENT_DATE, 'admin@promobuzz.com', 'Admin', 'Promobuzz', null, true, 
            'GdQXkV0ANF083lBeD4gXM+oeJtpph0mhMwnIU65nN6z9UaaPjQzpf9JvJquo8ycYIiR5gPfLOR2mJsNOY89mpDbwtB5RoOOC6iygBTtsdf22LP9svlh0Z+bqSEqukys/pHy8JOJNoP/SjsXtWOJfNPxzabKOdJUCvlXb6UmV7Sfb9y3OUWlFOG7FNqiHZoO6EqwXNYofk6P9yDF7JTIAiTu/nJsmRHxQ74ILVSliYjayOLpUKaj7dCN6JfCylk60iPs3Nt/UoC2duWdZ8NdwZe4PwSUy/R8oaiPJZrQsp5DjoEuwJWf8lfaZSeRe7Lmpn8mLEeWQJyDePSGJ9911eA==',
            '0F51eBH1IpPOLn5kXnNnd6+Scu8KmTw7TEFQj/sXSskOONSPzSNK71G7+uS4T8sy1sFVYPR0ks3Y+RSuGza61w==', 
            1, 1, null, null);
";

    #endregion

    private static string[] VariablesNames => new[] { "Id", "Name", "Code" };

    public static void Up(MigrationBuilder migrationBuilder) {
        migrationBuilder.Sql(InsertMFATypes(!migrationBuilder.IsNpgsql()));
        migrationBuilder.Sql(InsertCurrencies(!migrationBuilder.IsNpgsql()));
        migrationBuilder.Sql(InsertCountries);
        migrationBuilder.Sql(InsertCountryStates);
        migrationBuilder.Sql(InsertLocales);
        migrationBuilder.Sql(InsertObjectTypes);
        migrationBuilder.Sql(InsertTaskTypes);
        migrationBuilder.Sql(InsertAuditEventTypes);
        migrationBuilder.Sql(InsertTaskActionTypes);
        migrationBuilder.Sql(InsertTaskStateTypes);
        migrationBuilder.Sql(InsertFollowTypes);
        migrationBuilder.Sql(InsertTaskStateGroupTypes);
        migrationBuilder.Sql(InsertUserTypes);
    }

    private static string CommandInsertInto(bool     isSql,
                                            string   table,
                                            string[] variablesNames,
                                            string   query,
                                            string?  schemaName = null) {
        var body = "INSERT INTO ";
        table = isSql ? $"[{table}]" : $@"""{table}""";
        schemaName = string.IsNullOrWhiteSpace(schemaName) ? string.Empty : isSql ? $"[{schemaName}]" : schemaName;

        if (!string.IsNullOrWhiteSpace(schemaName))
            schemaName += ".";

        var schemaTableName = schemaName + table;
        body += schemaTableName;

        for (var i = 0; i < variablesNames.Length; i++) {
            var item = variablesNames[i];
            variablesNames[i] = isSql ? $"[{item}]" : $@"""{item}""";
        }

        var columnPreview = $"({string.Join(',', variablesNames)})";

        body += $@"
{columnPreview}
";

        body += $@"
{query}
";

        if (isSql)
            body += @"
 ";

        return body;
    }

    #region InsertMFATypes

    public static string InsertMFATypes(bool isSql, string? schemaName = null) {
        return CommandInsertInto(isSql, InsertMFATypesInfo.Table, VariablesNames, InsertMFATypesInfo.Query,
            schemaName);
    }

    private static class InsertMFATypesInfo {
        internal static readonly string Table = "MultiFactorAuthTypes";

        internal static readonly string Query = @"VALUES 
(1, 'None', 'None'), 
(2, 'Email', 'Email')";
    }

    #endregion

    #region InsertCurrencies

    public static string InsertCurrencies(bool isSql, string? schemaName = null) {
        return CommandInsertInto(isSql, InsertCurrenciesInfo.Table, new[] { "Id", "Code", "Name" },
            InsertCurrenciesInfo.Query, schemaName);
    }

    private static class InsertCurrenciesInfo {
        internal static readonly string Table = "Currencies";

        #region Qeury

        internal static readonly string Query = @"VALUES
    (784,  'AED', 'UAE Dirham')
    ,(971,  'AFN', 'Afghani')
    ,(8,  'ALL', 'Lek')
    ,(51,  'AMD', 'Armenian Dram')
    ,(532,  'ANG', 'Netherlands Antillean Guilder')
    ,(973,  'AOA', 'Kwanza')
    ,(32,  'ARS', 'Argentine Peso')
    ,(36,  'AUD', 'Australian Dollar')
    ,(533,  'AWG', 'Aruban Florin')
    ,(944,  'AZN', 'Azerbaijan Manat')
    ,(977,  'BAM', 'Convertible Mark')
    ,(52,  'BBD', 'Barbados Dollar')
    ,(50,  'BDT', 'Taka')
    ,(975,  'BGN', 'Bulgarian Lev')
    ,(48,  'BHD', 'Bahraini Dinar')
    ,(108,  'BIF', 'Burundi Franc')
    ,(60,  'BMD', 'Bermudian Dollar')
    ,(96,  'BND', 'Brunei Dollar')
    ,(68,  'BOB', 'Boliviano')
    ,(984,  'BOV', 'Mvdol')
    ,(986,  'BRL', 'Brazilian Real')
    ,(44,  'BSD', 'Bahamian Dollar')
    ,(64,  'BTN', 'Ngultrum')
    ,(72,  'BWP', 'Pula')
    ,(933,  'BYN', 'Belarusian Ruble')
    ,(84,  'BZD', 'Belize Dollar')
    ,(124,  'CAD', 'Canadian Dollar')
    ,(976,  'CDF', 'Congolese Franc')
    ,(947,  'CHE', 'WIR Euro')
    ,(756,  'CHF', 'Swiss Franc')
    ,(948,  'CHW', 'WIR Franc')
    ,(990,  'CLF', 'Unidad de Fomento')
    ,(152,  'CLP', 'Chilean Peso')
    ,(156,  'CNY', 'Yuan Renminbi')
    ,(170,  'COP', 'Colombian Peso')
    ,(970,  'COU', 'Unidad de Valor Real')
    ,(188,  'CRC', 'Costa Rican Colon')
    ,(931,  'CUC', 'Peso Convertible')
    ,(192,  'CUP', 'Cuban Peso')
    ,(132,  'CVE', 'Cabo Verde Escudo')
    ,(203,  'CZK', 'Czech Koruna')
    ,(262,  'DJF', 'Djibouti Franc')
    ,(208,  'DKK', 'Danish Krone')
    ,(214,  'DOP', 'Dominican Peso')
    ,(12,  'DZD', 'Algerian Dinar')
    ,(818,  'EGP', 'Egyptian Pound')
    ,(232,  'ERN', 'Nakfa')
    ,(230,  'ETB', 'Ethiopian Birr')
    ,(978,  'EUR', 'Euro')
    ,(242,  'FJD', 'Fiji Dollar')
    ,(238,  'FKP', 'Falkland Islands Pound')
    ,(826,  'GBP', 'Pound Sterling')
    ,(981,  'GEL', 'Lari')
    ,(936,  'GHS', 'Ghana Cedi')
    ,(292,  'GIP', 'Gibraltar Pound')
    ,(270,  'GMD', 'Dalasi')
    ,(324,  'GNF', 'Guinean Franc')
    ,(320,  'GTQ', 'Quetzal')
    ,(328,  'GYD', 'Guyana Dollar')
    ,(344,  'HKD', 'Hong Kong Dollar')
    ,(340,  'HNL', 'Lempira')
    ,(191,  'HRK', 'Kuna')
    ,(332,  'HTG', 'Gourde')
    ,(348,  'HUF', 'Forint')
    ,(360,  'IDR', 'Rupiah')
    ,(376,  'ILS', 'New Israeli Sheqel')
    ,(356,  'INR', 'Indian Rupee')
    ,(368,  'IQD', 'Iraqi Dinar')
    ,(364,  'IRR', 'Iranian Rial')
    ,(352,  'ISK', 'Iceland Krona')
    ,(388,  'JMD', 'Jamaican Dollar')
    ,(400,  'JOD', 'Jordanian Dinar')
    ,(392,  'JPY', 'Yen')
    ,(404,  'KES', 'Kenyan Shilling')
    ,(417,  'KGS', 'Som')
    ,(116,  'KHR', 'Riel')
    ,(174,  'KMF', 'Comorian Franc ')
    ,(408,  'KPW', 'North Korean Won')
    ,(410,  'KRW', 'Won')
    ,(414,  'KWD', 'Kuwaiti Dinar')
    ,(136,  'KYD', 'Cayman Islands Dollar')
    ,(398,  'KZT', 'Tenge')
    ,(418,  'LAK', 'Lao Kip')
    ,(422,  'LBP', 'Lebanese Pound')
    ,(144,  'LKR', 'Sri Lanka Rupee')
    ,(430,  'LRD', 'Liberian Dollar')
    ,(426,  'LSL', 'Loti')
    ,(434,  'LYD', 'Libyan Dinar')
    ,(504,  'MAD', 'Moroccan Dirham')
    ,(498,  'MDL', 'Moldovan Leu')
    ,(969,  'MGA', 'Malagasy Ariary')
    ,(807,  'MKD', 'Denar')
    ,(104,  'MMK', 'Kyat')
    ,(496,  'MNT', 'Tugrik')
    ,(446,  'MOP', 'Pataca')
    ,(929,  'MRU', 'Ouguiya')
    ,(480,  'MUR', 'Mauritius Rupee')
    ,(462,  'MVR', 'Rufiyaa')
    ,(454,  'MWK', 'Malawi Kwacha')
    ,(484,  'MXN', 'Mexican Peso')
    ,(979,  'MXV', 'Mexican Unidad de Inversion ,(UDI)')
    ,(458,  'MYR', 'Malaysian Ringgit')
    ,(943,  'MZN', 'Mozambique Metical')
    ,(516,  'NAD', 'Namibia Dollar')
    ,(566,  'NGN', 'Naira')
    ,(558,  'NIO', 'Cordoba Oro')
    ,(578,  'NOK', 'Norwegian Krone')
    ,(524,  'NPR', 'Nepalese Rupee')
    ,(554,  'NZD', 'New Zealand Dollar')
    ,(512,  'OMR', 'Rial Omani')
    ,(590,  'PAB', 'Balboa')
    ,(604,  'PEN', 'Sol')
    ,(598,  'PGK', 'Kina')
    ,(608,  'PHP', 'Philippine Peso')
    ,(586,  'PKR', 'Pakistan Rupee')
    ,(985,  'PLN', 'Zloty')
    ,(600,  'PYG', 'Guarani')
    ,(634,  'QAR', 'Qatari Rial')
    ,(946,  'RON', 'Romanian Leu')
    ,(941,  'RSD', 'Serbian Dinar')
    ,(643,  'RUB', 'Russian Ruble')
    ,(646,  'RWF', 'Rwanda Franc')
    ,(682,  'SAR', 'Saudi Riyal')
    ,(90,  'SBD', 'Solomon Islands Dollar')
    ,(690,  'SCR', 'Seychelles Rupee')
    ,(938,  'SDG', 'Sudanese Pound')
    ,(752,  'SEK', 'Swedish Krona')
    ,(702,  'SGD', 'Singapore Dollar')
    ,(654,  'SHP', 'Saint Helena Pound')
    ,(694,  'SLL', 'Leone')
    ,(706,  'SOS', 'Somali Shilling')
    ,(968,  'SRD', 'Surinam Dollar')
    ,(728,  'SSP', 'South Sudanese Pound')
    ,(930,  'STN', 'Dobra')
    ,(222,  'SVC', 'El Salvador Colon')
    ,(760,  'SYP', 'Syrian Pound')
    ,(748,  'SZL', 'Lilangeni')
    ,(764,  'THB', 'Baht')
    ,(972,  'TJS', 'Somoni')
    ,(934,  'TMT', 'Turkmenistan New Manat')
    ,(788,  'TND', 'Tunisian Dinar')
    ,(776,  'TOP', 'Pa’anga')
    ,(949,  'TRY', 'Turkish Lira')
    ,(780,  'TTD', 'Trinidad and Tobago Dollar')
    ,(901,  'TWD', 'New Taiwan Dollar')
    ,(834,  'TZS', 'Tanzanian Shilling')
    ,(980,  'UAH', 'Hryvnia')
    ,(800,  'UGX', 'Uganda Shilling')
    ,(840,  'USD', 'US Dollar')
    ,(997,  'USN', 'US Dollar ,(Next day)')
    ,(940,  'UYI', 'Uruguay Peso en Unidades Indexadas ,(UI)')
    ,(858,  'UYU', 'Peso Uruguayo')
    ,(927,  'UYW', 'Unidad Previsional')
    ,(860,  'UZS', 'Uzbekistan Sum')
    ,(928,  'VES', 'Bolívar Soberano')
    ,(704,  'VND', 'Dong')
    ,(548,  'VUV', 'Vatu')
    ,(882,  'WST', 'Tala')
    ,(950,  'XAF', 'CFA Franc BEAC')
    ,(951,  'XCD', 'East Caribbean Dollar')
    ,(960,  'XDR', 'SDR ,(Special Drawing Right)')
    ,(952,  'XOF', 'CFA Franc BCEAO')
    ,(953,  'XPF', 'CFP Franc')
    ,(994,  'XSU', 'Sucre')
    ,(965,  'XUA', 'ADB Unit of Account')
    ,(886,  'YER', 'Yemeni Rial')
    ,(710,  'ZAR', 'Rand')
    ,(967,  'ZMW', 'Zambian Kwacha')
    ,(932,  'ZWL', 'Zimbabwe Dollar')";

        #endregion
    }

    #endregion

    #region UserType

    public const string InsertUserTypes = @"
INSERT INTO ""UserTypes""(""Id"", ""Name"", ""Code"")
VALUES
 (1, 'Admin', 'Admin')
,(2, 'Customer', 'Customer')
,(3, 'Executor', 'Executor')
";

    //    CompanyAdmin = 4,
    // CompanyTaskResolver = 5
    public const string InsertUserTypesRefery = @"
INSERT INTO ""UserTypes""(""Id"", ""Name"", ""Code"")
VALUES
 (4, 'CompanyAdmin', 'CompanyAdmin')
,(5, 'CompanyTaskResolver', 'CompanyTaskResolver')
";

    public const string UpdateUserTypesResolverToManager = @"
UPDATE ""UserTypes""
SET ""Name"" = 'Company Task Manager', ""Code""= 'CompanyTaskManager'
WHERE ""Id"" = 5;
";

    public const string RevertUserTypesManagerToResolver = @"
UPDATE ""UserTypes""
SET ""Name"" = 'Company Task Resolver', ""Code""= 'CompanyTaskResolver'
WHERE ""Id"" = 5;
";

    #endregion
}

public static class MigrationBuilderExtension {
    public static void InsertTaskStateTypes(this MigrationBuilder migrationBuilder) {
        migrationBuilder.Sql(FillBaseDictionaries.InsertTaskStateTypes);
    }

    public static void InsertApiEndpointTypes(this MigrationBuilder migrationBuilder) {
        migrationBuilder.Sql(FillBaseDictionaries.InsertApiEndpointTypes);
    }

    public static void InsertObjectTypesV1(this MigrationBuilder migrationBuilder) {
        migrationBuilder.Sql(FillBaseDictionaries.InsertObjectTypes);
    }

    public static void InsertUserTypes(this MigrationBuilder migrationBuilder) {
        migrationBuilder.Sql(FillBaseDictionaries.InsertUserTypes);
    }

    public static void InsertUserReferyTypes(this MigrationBuilder migrationBuilder) {
        migrationBuilder.Sql(FillBaseDictionaries.InsertUserTypesRefery);
    }

    public static void InsertDictionaryItem(this MigrationBuilder migrationBuilder,
                                            string                table,
                                            int                   id,
                                            string                code,
                                            string                name) {
        migrationBuilder.Sql(@$"INSERT INTO ""{table}""(""Id"", ""Name"", ""Code"") VALUES ({id}, '{name}', '{code}')");
    }

    public static void InsertTransTaxSettingPropertyTypes(this MigrationBuilder migrationBuilder) {
        migrationBuilder.InsertDictionaryItem("SettingPropertyTypes", 2, "TransactionTaxWallet",
            "Transaction Tax Wallet");

        migrationBuilder.InsertDictionaryItem("SettingPropertyTypes", 3, "TransactionTaxCoins",
            "Trans action Tax Coins");

        // TransactionTaxWallet = 2,
        // TransactionTaxCoins = 3,
    }

    public static void InsertUserLogTypes(this MigrationBuilder migrationBuilder) {
        migrationBuilder.Sql(FillBaseDictionaries.InsertUserLogTypes);
    }
}