using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace Dictant.Web.Helpers
{
    public static class IJSAudioExtensions
    {
        public static async ValueTask AudioPlay(this IJSRuntime js,string id)
        { 
            await js.InvokeVoidAsync("AudioPlay",id);
        }
        public static async ValueTask AudioPause(this IJSRuntime js,string id)
        { 
            await js.InvokeVoidAsync("AudioPause",id);
        }
        public static async ValueTask AudioPlaySegment(this IJSRuntime js,string id,double start, double stop)
        { 
            await js.InvokeVoidAsync("AudioPlaySegment",id,start,stop);
        }
        public static async ValueTask AudioSetCurrentTime(this IJSRuntime js,string id,double start, double duration)
        { 
            await js.InvokeVoidAsync("AudioSetCurrentTime",id,start,duration);
        }
        public static async ValueTask<double> AudioGetCurrentTime(this IJSRuntime js,string id)
        {
            return await js.InvokeAsync<double>("AudioGetCurrentTime",id);
        }
        public static async ValueTask<double> AudioGetDuration(this IJSRuntime js,string id)
        {
            return await js.InvokeAsync<double>("AudioGetDuration",id);
        }
    }
}
