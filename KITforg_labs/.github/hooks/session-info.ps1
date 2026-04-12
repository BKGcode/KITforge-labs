# Script: Inject KITforge Lab state at session start
# Reads tools_lab_env.json and outputs a systemMessage for the agent.
# Exits silently (code 0) if the file doesn't exist or is outside the lab project.

$labFile = "Assets/Settings/KITforgeLabs/tools_lab_env.json"

if (-not (Test-Path $labFile)) {
    exit 0
}

try {
    $s = Get-Content $labFile -Raw | ConvertFrom-Json

    $activeProduct = if ($s.activeProduct) { $s.activeProduct } else { "none" }
    $labPhase = if ($s.labPhase) { $s.labPhase } else { "unknown" }

    $msg = @"
[KITforge Lab] Studio: $($s.studio) | Lab phase: $labPhase | Active product: $activeProduct
Bootstrap: read '$($s.bootstrapFile)' before making product changes.
Factory rule: no product code without approved BRIEF. No BUILD without approved ARCHITECTURE.
"@

    @{ systemMessage = $msg } | ConvertTo-Json -Compress
    exit 0
}
catch {
    exit 0
}
