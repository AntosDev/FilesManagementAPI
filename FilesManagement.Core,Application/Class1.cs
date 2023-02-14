namespace FilesManagement.Core_Application
{
    public class Class1
    {

    }

    private void GetTokenAndUserData(LoginRequestDTO loginDTO, out LogedInUserBE userBE, out string jwtToken)
    {
        var secret = _config["Jwt:Key"].ToString();
        userBE = new LogedInUserBE();
        jwtToken = _identityService.Login(loginDTO.Username, loginDTO.Password, secret, out userBE, _vendorSettings.RoleReference);
    }
    LogedInUserBE userBE;
    string jwtToken;
    GetTokenAndUserData(loginDTO, out userBE, out jwtToken);
    LogedinUserDTO userDTO = mapToUserDTO(userBE);
            return Ok(new AuthResultDTO()
    {
        Success = true,
                Token = jwtToken,
                User = userDTO
            });
}