<?php
declare(strict_types=1);
$phpVersion = PHP_VERSION;
$modules = get_loaded_extensions();
sort($modules, SORT_NATURAL | SORT_FLAG_CASE);
function escapeHtml(string $value): string {
    return htmlspecialchars($value, ENT_QUOTES | ENT_SUBSTITUTE, 'UTF-8');
}
?>
<!doctype html>
<html lang="en"><head><meta charset="utf-8"><meta name="viewport" content="width=device-width, initial-scale=1">
<title>PHP Runtime Information</title>
<style>
body{font-family:Arial,sans-serif;background:#f3f6fb;color:#172033;margin:0;padding:42px 20px}
main{max-width:850px;margin:auto;padding:34px;background:white;border-radius:16px;box-shadow:0 12px 32px #14233c1a}
h1{color:#262b78}.version{display:inline-block;padding:8px 14px;border-radius:20px;background:#e8eaff;color:#262b78;font-weight:bold}
ul{columns:2;column-gap:40px;padding-left:22px}li{margin-bottom:8px;break-inside:avoid}
@media(max-width:560px){ul{columns:1}}
</style></head><body><main>
<h1>PHP Runtime Information</h1>
<p class="version">PHP version: <?= escapeHtml($phpVersion) ?></p>
<h2>Loaded PHP modules (<?= count($modules) ?>)</h2><ul>
<?php foreach ($modules as $module): ?><li><?= escapeHtml($module) ?></li><?php endforeach; ?>
</ul></main></body></html>
