using CFlix.Models;
using CFlix.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CFlix.Data
{
    public class CFlixAuthInitializer
    {
        private readonly ILogger<CFlixAuthInitializer> _logger;
        private readonly UserManager<CFlixUser> _userManager;
        private readonly CFlixAuthContext _context;
        private readonly CFlixConfiguration _conf;

        public CFlixAuthInitializer(
            UserManager<CFlixUser> userManager,
            CFlixAuthContext context,
            ILoggerFactory loggerFactory,
            IOptions<CFlixConfiguration> conf)
        {
            _userManager = userManager;
            _context = context;
            _conf = conf.Value;
            _logger = loggerFactory.CreateLogger<CFlixAuthInitializer>();
        }

        public async Task Seed()
        {
            _logger.LogInformation("Update database {0}", _context.Database.GetDbConnection().DataSource);
            _context.Database.Migrate();
            _logger.LogInformation("Database migration successfull");

            _logger.LogInformation("SeedStep1");
            await SeedStep1();
            _logger.LogInformation("SeedStep1 finished");

            if (_conf.Stage >= 2)
            {
                _logger.LogInformation("SeedStep2");
                await SeedStep2();
                _logger.LogInformation("SeedStep2 finished");
            }

            if (_conf.Stage >= 3)
            {
                _logger.LogInformation("SeedStep3");
                await SeedStep3();
                _logger.LogInformation("SeedStep3 finished");
            }
        }
        
        private async Task SeedStep1()
        {
            #region User
            var admin = await CreateUser(new CFlixUser
            {
                UserName = "cflix.administrateur",
                Email = "cflix.administrateur@CFlix.com",
                EmployeeID = "Story_684321",
                DisplayName = "SuperAdmin",
                AvatarType = AvatarType.Avatar0,
                AccountType = AccountType.Alpha
            });

            var hackonymousoflix = await CreateUser(new CFlixUser
            {
                UserName = "hackonymousoflix",
                Email = "hackonymousoflix@anonymous.com",
                EmployeeID = "Story_12345",
                DisplayName = "Vilain petit canard",
                AvatarType = AvatarType.Avatar2,
                AccountType = AccountType.Alpha
            });

            await CreateUser(new CFlixUser
            {
                UserName = "chuck.mcgill",
                Email = "chuck.mcgill@hhm.com",
                EmployeeID = "Story_35458",
                DisplayName = "Chuck",
                AvatarType = AvatarType.Avatar5,
                AccountType = AccountType.Beta
            });

            await CreateUser(new CFlixUser
            {
                UserName = "mike.ehrmantraut",
                Email = "mike.ehrmantraut@bettercallsaul.com",
                EmployeeID = "Story_95354",
                DisplayName = "Mike",
                AvatarType = AvatarType.Avatar7,
                AccountType = AccountType.Beta
            });

            await CreateUser(new CFlixUser
            {
                UserName = "jon.snow",
                Email = "jon.snow@gameofthrones.com",
                EmployeeID = "Story_342534",
                DisplayName = "Jon Snow",
                AvatarType = AvatarType.Avatar8,
                AccountType = AccountType.Beta
            });

            await CreateUser(new CFlixUser
            {
                UserName = "daenerys.targaryen",
                Email = "daenerys.targaryen@gameofthrones.com",
                EmployeeID = "Story_35740",
                DisplayName = "Daenerys Targaryen",
                AvatarType = AvatarType.Avatar6,
                AccountType = AccountType.Beta
            });

            var badger = await CreateUser(new CFlixUser
            {
                UserName = "brandon.mayhew",
                Email = "brandon.mayhew@breakingbad.com",
                EmployeeID = "Story_12543",
                DisplayName = "Badger",
                AvatarType = AvatarType.Avatar1,
                AccountType = AccountType.Beta
            });

            await CreateUser(new CFlixUser
            {
                UserName = "hank.schrader",
                Email = "hank.schrader@breakingbad.com",
                EmployeeID = "Story_864852",
                DisplayName = "Hank Schrader",
                AvatarType = AvatarType.Avatar4,
                AccountType = AccountType.Beta
            });
            await CreateUser(new CFlixUser
            {
                UserName = "waldo",
                Email = "waldo@blackmirror.com",
                EmployeeID = "Story_01734",
                DisplayName = "Waldo",
                AvatarType = AvatarType.Avatar7,
                AccountType = AccountType.Beta
            });
            #endregion

            #region EasterEgg
            // Les challs Bonus
            var challBonus1 = await CreateEasterEgg(new EasterEgg("Jeu d'enfant",
                EasterEggType.Bonus,
                "Le plus simple de tous à débloquer",
                "49fdbf1ca802408243d0e5a76fff553de70387f996564998d4561a73b086239e")
            { IsAvailable = true });

            var challBonus2 = await CreateEasterEgg(new EasterEgg("L'éthique des barmans",
                EasterEggType.Bonus,
                "Parce qu'On Sait Tituber En Rigolant",
                "c1f10fa4ee420d6efe0b862487e0e707aaab2ad6c29a04c67fd192e18b3e0208")
            { IsAvailable = true });

            var challBonus3 = await CreateEasterEgg(new EasterEgg("Lu et approuvé",
                EasterEggType.Bonus,
                "Un oeuf peut en cacher un autre",
                "b1629c48a75421e3debab3efbec8d7135509c484541779f6f01ea68ca47cd931")
            { IsAvailable = true });

            var challBonus4 = await CreateEasterEgg(new EasterEgg("It's so good man",
                EasterEggType.Bonus,
                "Et Xerox Identifia gustavo Fring",
                "5c88ec59652389de918175f43d9deeafb0f7fbf5daf685e1c53ebe353bd4fde9")
            { IsAvailable = true });

            var challBonus5 = await CreateEasterEgg(new EasterEgg("We need a genius",
                EasterEggType.Bonus,
                "Il semblerait que l'image soit brouillée",
                "b5cb9bc1de536a0b0aa640a0428bda6f91e9b865e9d639b202d1511aaaedd48c")
            { IsAvailable = true });

            // Les challs Step 01
            var chall1 = await CreateEasterEgg(new EasterEgg("Avocat incontestable",
                EasterEggType.Challenge,
                "A trouvé mon premier easter egg - Hackonymousoflix",
                "82ceaea9036cf0d3ebf3fe9a46ba99655a3b32c4b3694d5e3a4e8f6d7c40c018")
            { IsAvailable = true });

            var chall2 = await CreateEasterEgg(new EasterEgg("Jeu de pouvoir",
                EasterEggType.Challenge,
                "Tu sais analyser les codes malicieux - Hackonymousoflix",
                "a80e1447a45862af34e46b3324a41730d9099c7dd66ea62f3071f998fd7e67a1")
            { IsAvailable = true });

            var chall3 = await CreateEasterEgg(new EasterEgg("BugFinder",
                EasterEggType.Challenge,
                "A aidé l'équipe CFlix en nous communiquant un bug",
                "fc6764362726163696cb571891b4f16811d1f35422349137cbf45a84142f5553")
            { IsAvailable = true });

            var chall4 = await CreateEasterEgg(new EasterEgg("Dark reflection",
                EasterEggType.Challenge,
                "Etait-ce si bien caché que ça ? - Hackonymousoflix",
                "1b9058653f746a17efcaf7feeecf688832a7c6e9a71a36cd1d90429d18751f32")
            { IsAvailable = true });


            // Les challs Step 02
            var chall5 = await CreateEasterEgg(new EasterEgg("Goût du risque",
                EasterEggType.Challenge,
                "J'aime quand un plan se déroule sans accroc !",
                "378ce825abef54ca40b1132a33fab6bd74a1b1554b8c427425490ddeeb09c728"));

            var chall6 = await CreateEasterEgg(new EasterEgg("It's For Life",
                EasterEggType.Challenge,
                "Inclure des zombies n'était pas une si bonne idée - Hackonymousoflix",
                "16e85cafdf5f34c2b605302adb002d1fa729a4e1ab6967c3d18e60b7f9136e62"));

            var chall7 = await CreateEasterEgg(new EasterEgg("Oh mon Dieu ! Ils ont tué Kenny !!",
                EasterEggType.Challenge,
                "Avec keepass il n'y aurait rien eu - Hackonymousoflix",
                "aafbc4967bfa56ab5a4a9bf102e79d8fbeda42fe77d3a484a40f273f42407a91"));

            var chall8 = await CreateEasterEgg(new EasterEgg("Danger zone",
                EasterEggType.Challenge,
                "J'avais quelque chose pour ça - Hackonymousoflix",
                "56d7374ec8f441c20a0314d06f052b9644bfb4bf411fbb672147d6d038158d8c"));


            // Les challs Step 03
            var chall9 = await CreateEasterEgg(new EasterEgg("La contre-attaque", // SnK faille cookie -> mdp du certificat de connexion
                EasterEggType.Challenge,
                "Si tu gagnes, tu vies. Si tu perds, tu meurs. Si tu ne te bats pas, tu ne peux pas gagner !",
                "2c38271b48f19adb76141543c33c36fdeb7c77b473492dd255c89f839bac5f49"));

            var chall10 = await CreateEasterEgg(new EasterEgg("La vérité éclate", // HoC launchnb 
                EasterEggType.Challenge,
                "Nous avons identifié qui est hackonymousoflix !",
                "2fe1da96abac3d04780d57c5e6f9789e4ed41c6d557e008c3f8619e7754a977f"));
            
            var chall11 = await CreateEasterEgg(new EasterEgg("C étrange tout cela", // ST ecrit dans la todolist
                EasterEggType.Challenge,
                "Mais que doit-il donc faire de si important ??",
                "f573f23afbadb75a92d69b4786f2177112d37ee17a39f1411130d4c9ae83b20e"));
            
            var chall12 = await CreateEasterEgg(new EasterEgg("La boucle est bouclée", // M.R a récupérer sur le shop
                EasterEggType.Challenge,
                "Et devient calife à la place du calife !",
                "42c86cbc31481423221c043c66c2049faff4b08bf7c1ed3be639f3cae189abbf"));

            await _context.SaveChangesAsync();
            _logger.LogInformation("EasterEggs successfully added");
            #endregion

            #region UserEasterEgg
            if (!await _context.UserEasterEggs.AnyAsync())
            {
                await _context.UserEasterEggs.AddRangeAsync(
                    new CFlixUserEasterEgg
                    {
                        CFlixUser = admin,
                        EasterEgg = challBonus1,
                        CreationDate = new DateTime(2017, 04, 1, 1, 13, 37)
                    },
                    new CFlixUserEasterEgg
                    {
                        CFlixUser = admin,
                        EasterEgg = chall3,
                        CreationDate = new DateTime(2017, 04, 1, 1, 13, 37)
                    },
                    new CFlixUserEasterEgg
                    {
                        CFlixUser = badger,
                        EasterEgg = challBonus1,
                        CreationDate = new DateTime(2017, 04, 23, 6, 32, 17)
                    },
                    new CFlixUserEasterEgg
                    {
                        CFlixUser = hackonymousoflix,
                        EasterEgg = chall3,
                        CreationDate = new DateTime(2017, 05, 28, 23, 38, 2)
                    },
                    new CFlixUserEasterEgg
                    {
                        CFlixUser = hackonymousoflix,
                        EasterEgg = chall1,
                        CreationDate = new DateTime(2017, 05, 29, 03, 14, 43)
                    },
                    new CFlixUserEasterEgg
                    {
                        CFlixUser = hackonymousoflix,
                        EasterEgg = chall2,
                        CreationDate = new DateTime(2017, 05, 29, 04, 47, 26)
                    },
                    new CFlixUserEasterEgg
                    {
                        CFlixUser = hackonymousoflix,
                        EasterEgg = chall4,
                        CreationDate = new DateTime(2017, 05, 29, 06, 10, 2)
                    });
                await _context.SaveChangesAsync();
                _logger.LogInformation("UserEasterEggs successfully added");
            }
            #endregion
        }

        public async Task SeedStep2()
        {
            #region User
            await CreateUser(new CFlixUser
            {
                UserName = "bosco.albert.baracus",
                Email = "bosco.albert.baracus@ateam.com",
                EmployeeID = "Story_35765",
                DisplayName = "Barracuda",
                AvatarType = AvatarType.Avatar1,
                AccountType = AccountType.Beta
            });

            await CreateUser(new CFlixUser
            {
                UserName = "henry.murdock",
                Email = "henry.murdock@ateam.com",
                EmployeeID = "Story_66245",
                DisplayName = "Looping",
                AvatarType = AvatarType.Avatar4,
                AccountType = AccountType.Beta
            });

            await CreateUser(new CFlixUser
            {
                UserName = "lucille",
                Email = "lucille@thewalkingdead.com",
                EmployeeID = "Story_53775",
                DisplayName = "Lucille",
                AvatarType = AvatarType.Avatar1,
                AccountType = AccountType.Beta
            });

            await CreateUser(new CFlixUser
            {
                UserName = "eugene.porter",
                Email = "eugene.porter@thewalkingdead.com",
                EmployeeID = "Story_63711",
                DisplayName = "Eugène",
                AvatarType = AvatarType.Avatar5,
                AccountType = AccountType.Beta
            });

            await CreateUser(new CFlixUser
            {
                UserName = "kenny.mccormick",
                Email = "kenny.mccormick@southpark.com",
                EmployeeID = "Story_45681",
                DisplayName = "Kenny",
                AvatarType = AvatarType.Avatar8,
                AccountType = AccountType.Beta
            });

            await CreateUser(new CFlixUser
            {
                UserName = "herbert.garrison",
                Email = "herbert.garrison@southpark.com",
                EmployeeID = "Story_88731",
                DisplayName = "M. President Garrison",
                AvatarType = AvatarType.Avatar5,
                AccountType = AccountType.Beta
            });

            await CreateUser(new CFlixUser
            {
                UserName = "sterling.archer",
                Email = "sterling.archer@isis.com",
                EmployeeID = "Story_93351",
                DisplayName = "Archer",
                AvatarType = AvatarType.Avatar1,
                AccountType = AccountType.Beta
            });

            await CreateUser(new CFlixUser
            {
                UserName = "lana.kane",
                Email = "lana.kane@isis.com",
                EmployeeID = "Story_98776",
                DisplayName = "Lana",
                AvatarType = AvatarType.Avatar3,
                AccountType = AccountType.Beta
            });
            #endregion

            #region EasterEgg
            var chall5 = await CreateEasterEgg(new EasterEgg { Hash = "378ce825abef54ca40b1132a33fab6bd74a1b1554b8c427425490ddeeb09c728" });
            chall5.IsAvailable = true;
            var chall6 = await CreateEasterEgg(new EasterEgg { Hash = "16e85cafdf5f34c2b605302adb002d1fa729a4e1ab6967c3d18e60b7f9136e62" });
            chall6.IsAvailable = true;
            var chall7 = await CreateEasterEgg(new EasterEgg { Hash = "aafbc4967bfa56ab5a4a9bf102e79d8fbeda42fe77d3a484a40f273f42407a91" });
            chall7.IsAvailable = true;
            var chall8 = await CreateEasterEgg(new EasterEgg { Hash = "56d7374ec8f441c20a0314d06f052b9644bfb4bf411fbb672147d6d038158d8c" });
            chall8.IsAvailable = true;
            await _context.SaveChangesAsync();
            _logger.LogInformation("Change EasterEggs availability successfully");
            #endregion

            #region UserEasterEgg
            var hackonymousoflix = await _userManager.FindByNameAsync("hackonymousoflix");

            await CreateUserEasterEgg(new CFlixUserEasterEgg
            {
                CFlixUser = hackonymousoflix,
                EasterEgg = chall5,
                CreationDate = new DateTime(2017, 06, 04, 12, 35, 18)
            });

            await CreateUserEasterEgg(new CFlixUserEasterEgg
            {
                CFlixUser = hackonymousoflix,
                EasterEgg = chall6,
                CreationDate = new DateTime(2017, 06, 04, 23, 44, 1)
            });

            await CreateUserEasterEgg(new CFlixUserEasterEgg
            {
                CFlixUser = hackonymousoflix,
                EasterEgg = chall7,
                CreationDate = new DateTime(2017, 06, 05, 01, 10, 10)
            });

            await CreateUserEasterEgg(new CFlixUserEasterEgg
            {
                CFlixUser = hackonymousoflix,
                EasterEgg = chall8,
                CreationDate = new DateTime(2017, 06, 05, 02, 25, 45)
            });

            await _context.SaveChangesAsync();
            _logger.LogInformation("UserEasterEggs successfully added");
            #endregion

        }

        public async Task SeedStep3()
        {
            #region User
            await CreateUser(new CFlixUser
            {
                UserName = "stuart.bloom",
                Email = "stuart.bloom@bigbangtheory.com",
                EmployeeID = "Story_537448",
                DisplayName = "Stuart",
                AvatarType = AvatarType.Avatar1,
                AccountType = AccountType.Beta
            });

            await CreateUser(new CFlixUser
            {
                UserName = "sheldon.cooper",
                Email = "sheldon.cooper@bigbangtheory.com",
                EmployeeID = "Story_537448",
                DisplayName = "Sheldor le Conquérant",
                AvatarType = AvatarType.Avatar5,
                AccountType = AccountType.Beta
            });

            await CreateUser(new CFlixUser
            {
                UserName = "howard.wolowitz",
                Email = "howard.wolowitz@bigbangtheory.com",
                EmployeeID = "Story_873451",
                DisplayName = "Howi",
                AvatarType = AvatarType.Avatar4,
                AccountType = AccountType.Beta
            });
            
            await CreateUser(new CFlixUser
            {
                UserName = "hange.zoe",
                Email = "hange.zoe@eldia.com",
                EmployeeID = "Story_373473",
                DisplayName = "Hange",
                AvatarType = AvatarType.Avatar6,
                AccountType = AccountType.Beta
            });

            await CreateUser(new CFlixUser
            {
                UserName = "armin.arlert",
                Email = "armin.arlert@eldia.com",
                EmployeeID = "Story_837432",
                DisplayName = "Armin",
                AvatarType = AvatarType.Avatar5,
                AccountType = AccountType.Beta
            });

            await CreateUser(new CFlixUser
            {
                UserName = "francis.joseph.underwood",
                Email = "frank.underwood@whitehouse.com",
                EmployeeID = "Story_373923",
                DisplayName = "M. President",
                AvatarType = AvatarType.Avatar7,
                AccountType = AccountType.Beta
            });

            await CreateUser(new CFlixUser
            {
                UserName = "claire.underwood",
                Email = "claire.underwood@whitehouse.com",
                EmployeeID = "Story_839432",
                DisplayName = "Claire Underwood",
                AvatarType = AvatarType.Avatar3,
                AccountType = AccountType.Beta
            });

            await CreateUser(new CFlixUser
            {
                UserName = "jane.ives",
                Email = "jane.ives@strangerthings.com",
                EmployeeID = "Story_328923",
                DisplayName = "Eleven",
                AvatarType = AvatarType.Avatar3,
                AccountType = AccountType.Beta
            });

            await CreateUser(new CFlixUser
            {
                UserName = "mike.wheeler",
                Email = "mike.wheeler@strangerthings.com",
                EmployeeID = "Story_833472",
                DisplayName = "Mike",
                AvatarType = AvatarType.Avatar4,
                AccountType = AccountType.Beta
            });
            #endregion

            #region EasterEgg
            var chall9 = await CreateEasterEgg(new EasterEgg { Hash = "2c38271b48f19adb76141543c33c36fdeb7c77b473492dd255c89f839bac5f49" });
            chall9.IsAvailable = true;
            var chall10 = await CreateEasterEgg(new EasterEgg { Hash = "2fe1da96abac3d04780d57c5e6f9789e4ed41c6d557e008c3f8619e7754a977f" });
            chall10.IsAvailable = true;
            var chall11 = await CreateEasterEgg(new EasterEgg { Hash = "f573f23afbadb75a92d69b4786f2177112d37ee17a39f1411130d4c9ae83b20e" });
            chall11.IsAvailable = true;
            var chall12 = await CreateEasterEgg(new EasterEgg { Hash = "42c86cbc31481423221c043c66c2049faff4b08bf7c1ed3be639f3cae189abbf" });
            chall12.IsAvailable = true;
            await _context.SaveChangesAsync();
            _logger.LogInformation("Change EasterEggs availability successfully");
            #endregion
        }

        private async Task CreateUserEasterEgg(CFlixUserEasterEgg ueeg)
        {
            if (!await _context.UserEasterEggs.AnyAsync(uee => uee.CFlixUser == ueeg.CFlixUser && uee.EasterEgg == ueeg.EasterEgg))
            {
                var result = await _context.UserEasterEggs.AddAsync(ueeg);
            }
        }

        private async Task<CFlixUser> CreateUser(CFlixUser usr)
        {
            if ((await _userManager.FindByNameAsync(usr.UserName)) == null)
            {
                var result = await _userManager.CreateAsync(usr);
                _logger.LogInformation("User {0} created", usr.UserName);
            }

            return usr;
        }

        private async Task<EasterEgg> CreateEasterEgg(EasterEgg easterEgg)
        {
            var egg = await _context.EasterEggs.FirstOrDefaultAsync(ee => ee.Hash == easterEgg.Hash);

            if (egg == null)
            {
                await _context.AddAsync(easterEgg);
                await _context.SaveChangesAsync();
                _logger.LogInformation("EasterEgg {0} added", easterEgg.Title);
                return easterEgg;
            }

            return egg;
        }
    }
}
