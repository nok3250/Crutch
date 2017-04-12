# Crutch

Updater.exe для автообновления файлов под Windows

Технологии:
1. c#
2. Visual Studio 2015

#### Установка

1. Установите `Visual Studio 2015`
2. Склонируйте репозиторий
3. Откройте решение из папки `Crutch`
4. Соберите решение

#### Запуск

Вызов программы `updater.exe {from_file_path} {to_file_path} {after_copy_launth_exe_file_path} {timeout_miliseconds}`

---

## Пример использования

#### Обновления app.asar Electron приложений

Идея была взята на основе [electron-basic-updater](https://github.com/TamkeenLMS/electron-basic-updater)
Но существет проблема при обновлении под Windows. Эти файлы заняты процессом, и их нельзя заменить.
Для такого и был придуман Crutch (electron windows basic updater).

Идея заключается в обновлении не всей программы на electron, а только asar файлов, 
в которых собственно и заложена вся логика приложения. таким образом можно 
сократить размер обновление с 50-100мб до 5-10мб.


        const electron = require('electron');
        const app = electron.app;
        const os = require('os').platform();
        const spawn = require('child_process').spawn;
        
        if (os == 'win32') {
            const updateProcess = spawn('update.exe', ['upadate\app.asar', 'resources\app.asar', 'electron.exe', 5000]);
            app.quit();
        }
        
         
#### nodejs

        const spawn = require('child_process').spawn;

        const updateProcess = spawn('update.exe', ['upadate\app.asar', 'resources\app.asar']);
        
---
        
The MIT License (MIT) - Copyright (c) 2017 nok3250 artem.korda@gmail.com