param(
	[switch]$Recreate
)

$ErrorActionPreference = "Stop"

Write-Host "[SalesOps] Docker ortamı hazırlanıyor..." -ForegroundColor Cyan

if (-not (Get-Command docker -ErrorAction SilentlyContinue)) {
	throw "Docker CLI bulunamadı. Docker Desktop kurup tekrar deneyin."
}

try {
	docker info | Out-Null
}
catch {
	throw "Docker çalışmıyor. Docker Desktop'ı açıp tekrar deneyin."
}

$repoRoot = Split-Path -Parent $PSScriptRoot
Set-Location $repoRoot

Write-Host "[SalesOps] İmajlar çekiliyor..." -ForegroundColor Yellow
docker compose pull

if ($Recreate) {
	Write-Host "[SalesOps] Container'lar recreate ediliyor..." -ForegroundColor Yellow
	docker compose up -d --force-recreate
}
else {
	Write-Host "[SalesOps] Container'lar ayağa kaldırılıyor..." -ForegroundColor Yellow
	docker compose up -d
}

Write-Host "`n[SalesOps] Çalışan servisler:" -ForegroundColor Green
docker compose ps

Write-Host "`nTamam. Yeni bilgisayarda tek komutla kurulumu tamamladınız." -ForegroundColor Green
