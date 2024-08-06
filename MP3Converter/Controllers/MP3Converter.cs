using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YoutubeExplode;
using YoutubeExplode.Converter;
using YoutubeExplode.Videos.Streams;

namespace MP3Converter.Controllers
{

    [ApiController]
    [Route("/api/mp3")]
    public class MP3Converter : ControllerBase
    {

        private readonly YoutubeClient YTClient;

        public MP3Converter()
        {
            //initialize client
            YTClient = new YoutubeClient();


        }


        //get video metadata for displaying
        [HttpGet("videoInfo")]
        public async Task<IActionResult> getMp3Data(string Url)
        {

            if (Url == null)
            {

                return BadRequest("Invalid URL or video not found");

            }

            try
            {
                var video = await YTClient.Videos.GetAsync(Url);

                var videoInfo = new
                {
                    Title = video.Title,
                    Duration = video.Duration

                };

                return Ok(videoInfo);

            }

            catch (Exception ex)
            {

                return BadRequest(ex.Message);

            }



        }
    


        

        [HttpGet("download")]
        public async Task<ActionResult> DownloadMp3(string Url)
        {
            if (Url == null)
            {
                return BadRequest("Invalid URL or Video not found");
            }

            try
            {
                //retrieve stream metadata
                var streamManifest = await YTClient.Videos.Streams.GetManifestAsync(Url);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
    }
}

       

