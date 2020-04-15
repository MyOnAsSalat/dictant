runCaptcha = function (actionName) {
    return new Promise((resolve, reject) => {
        grecaptcha.ready(function () {
            grecaptcha.execute('6Lfk9ucUAAAAAD2RXe_7XBxLtV4oJ2lj09SVg1yr', { action: actionName })
                .then(function (token) {
                    console.log("token: " + token);
                    resolve(token);
                });
        });

    });
};


