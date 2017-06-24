(function ()
{
    function LoginModel()
    {
        var self = this;
        self.login = ko.observable();
        self.pwd = ko.observable();

        self.checkLoginPwd = function ()
        {
            alertify.message("Auth ...");
            $.get(
                //"/api/auth/login",
                "LoginAuth.aspx",
                { login: self.login(), pwd: self.pwd(), rnd: Math.random(), action: "login" })
                .done(function (data)
                {
                    if (data && data.Status == "Error")
                        return alertify.error("Invalid credentials.");

                    alertify.success("Success, redirect to main page.")

                    location.href = "/";
                })
                .error(function()
                {
                    alertify.error("Invalid credentials.");
                });
        };

        self.checkLoginByEnter = function(model, ev)
        {
            if (ev.keyCode != 13) return;
            self.checkLoginPwd();
        }
    }

    $(document).ready(function ()
    {
        ko.applyBindings(new LoginModel());
    });
})();