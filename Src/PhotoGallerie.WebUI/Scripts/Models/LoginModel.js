(function ()
{
    function LoginModel()
    {
        var self = this;
        self.login = ko.observable();
        self.pwd = ko.observable();

        self.checkLoginPwd = function ()
        {
            $.post("/api/auth/login", { login: self.login(), pwd: self.pwd() })
                .done(function (data)
                {
                    console.log(data);
                });
        };
    }

    $(document).ready(function ()
    {
        ko.applyBindings(new LoginModel());
    });
})();