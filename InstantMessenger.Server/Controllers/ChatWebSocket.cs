using System.Net.WebSockets;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace InstantMessenger.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatWebSocketController : ControllerBase
    {
        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            if(!HttpContext.WebSockets.IsWebSocketRequest)
                return BadRequest("Web socket request expected");

            using var ws = await HttpContext.WebSockets.AcceptWebSocketAsync();
            while(true){

                if(ws.State == WebSocketState.Closed || ws.State == WebSocketState.Aborted)
                    break;

                var message = DateTime.Now.ToString("HH:mm:ss");
                var bytes = Encoding.UTF8.GetBytes(message);

                var arraySegment = new ArraySegment<byte>(bytes, 0, bytes.Length);

                if(ws.State == WebSocketState.Open){
                    await ws.SendAsync(arraySegment, WebSocketMessageType.Text, true, CancellationToken.None);
                }
                Thread.Sleep(1000);
            }   

            await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None); 

            return Ok();
        }
    }
}
