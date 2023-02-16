

using Identity.Infra;

namespace Identity.Tests
{
    public class PasswordHasherTests
    {
        [Fact]
        public void PasswordHashesWell()
        {
            var passwordHasher = new PasswordHasher();
            Assert.True(passwordHasher.Check("10|Jf2tWxMCxFxb/UPkHnxZqA==|NsYGhfw4NYyiWLsSxrTISNAB1SYmrinumZTbaBT7wms=","P@ssw0rd"));
        }
    }
}
