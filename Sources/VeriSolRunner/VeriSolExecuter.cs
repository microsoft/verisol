using Microsoft.Extensions.Logging;
using System;

namespace VerisolRunnerWithSpecs
{
    internal class VeriSolExecuterWithSpecs
    {
        private string specFilePath;
        private string contractPath;
        private string contractDir;
        private string corralPath;
        private string solcPath;
        private string solcName;
        private ILogger logger;

        public VeriSolExecuterWithSpecs(string specFilePath, string contractPath, string contractDir, string corralPath, string solcPath, string solcName, ILogger logger)
        {
            this.specFilePath = specFilePath;
            this.contractPath = contractPath;
            this.contractDir = contractDir;
            this.corralPath = corralPath;
            this.solcPath = solcPath;
            this.solcName = solcName;
            this.logger = logger;
        }

        public bool CopyTargetContractFolder()
        {
            throw new NotImplementedException();
        }

        public bool RewritePrivateVariables()
        {
            throw new NotImplementedException();
        }

        public bool ExecuteSolToBoogie()
        {
            throw new NotImplementedException();
        }

        public bool ExecuteCorral()
        {
            throw new NotImplementedException();
        }
    }
}