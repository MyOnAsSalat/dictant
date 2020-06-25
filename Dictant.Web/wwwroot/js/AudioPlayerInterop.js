AudioPlay = function (id) {
    AudioRemoveTimeout(id);
    var audio = document.getElementById(id);
    audio.play();
}
AudioPause = function (id) {
    AudioRemoveTimeout(id);
    var audio = document.getElementById(id);
    audio.pause();
}
AudioSetCurrentTime = function (id,time) {
    AudioRemoveTimeout(id);
    var audio = document.getElementById(id);
    audio.currentTime = time;
}
AudioGetCurrentTime = function (id) {
    var audio = document.getElementById(id);
    return audio.currentTime;
}
AudioPlaySegment = function (id,start,duration) {
    AudioRemoveTimeout(id);
    var audio = document.getElementById(id);
    audio.currentTime = start;
    audio.play();
    var timeoutId = setTimeout(function(){audio.pause()}, duration*1000);
    AudioTimeouts[id] =timeoutId;
}
AudioGetDuration = function (id) {
    var audio = document.getElementById(id);
    return audio.duration;
}
var AudioTimeouts = {};
AudioRemoveTimeout = function (audioId) {
    if (AudioTimeouts[audioId] !=undefined) {
        clearTimeout(AudioTimeouts[audioId]);
        AudioTimeouts[audioId] = undefined;
    }
}

