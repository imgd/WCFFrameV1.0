using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCF.Inspector
{
    public class BaseClientCheckFactory : IClientCheck
    {
        private void InitTokens()
        {
            _tokens = ClientIdentityKey.GetClientKeys();
            _tokenKeys = ClientIdentityKey.GetClientTokenKeys();
            ClientTokenTool = new ClientTokens(_tokens, _tokenKeys);
        }

        public BaseClientCheckFactory(string clientToken)
        {
            this.ClientToken = clientToken;
            InitTokens();
        }

        public bool ClientIdentityCheck()
        {
            return ClientTokenTool.CheckClientToken(ClientToken);
        }


        private ClientTokens ClientTokenTool
        {
            get;
            set;
        }

        private string ClientToken
        {
            get;
            set;
        }

        private Dictionary<string, string> _tokens;
        Dictionary<string, string> IClientCheck.Tokens
        {
            get
            {
                return _tokens;
            }
            set
            {
                _tokens = value;
            }
        }

        private string[] _tokenKeys;
        public string[] TokenKeys
        {
            get
            {
                return _tokenKeys;
            }
            set
            {
                _tokenKeys = value;
            }
        }
    }
}
