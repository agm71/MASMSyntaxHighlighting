using Microsoft.VisualStudio.Shell;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using Task = System.Threading.Tasks.Task;

namespace MASMSyntaxHighlighting
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [Guid(MASMSyntaxHighlightingPackage.PackageGuidString)]
    public sealed class MASMSyntaxHighlightingPackage : AsyncPackage
    {
        public const string PackageGuidString = "a27ee1fe-ce57-44e1-981d-f25cc77e3759";

        #region Package Members

        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await this.JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
        }

        #endregion
    }
}
