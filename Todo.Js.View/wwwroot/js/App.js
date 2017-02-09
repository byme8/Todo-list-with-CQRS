var http = function () {
    var scope = new Object();

    scope.get = function (url, success = null, error = null) {
        var request = new XMLHttpRequest();
        request.open('GET', url);
        request.send(null);
        request.onreadystatechange = function () {
            var DONE = 4;
            var OK = 200;
            if (request.readyState === DONE) {
                if (request.status === OK)
                    if (request.responseText) {
                        success(JSON.parse(request.responseText));
                    } else {
                        success(null);
                    }
            } else {
                if (error) {
                    error(request.status);
                }
            }
        }
    };

    return scope;
} ();

var TodoService = function () {
    var scope = new Object();
    var tasks = null;
    var finishedTasksVisible = true;
    var add = function (message) {
        http.get("http://localhost:5001/api/todo/add/" + message, function (responce) {
            scope.get();
        });
    };

    var setupTasks = function (responce) {
        var todolist = document.getElementById("todolist");
        var todolistHtml = "";

        for (var i = 0; i < responce.length; i++) {
            var task = responce[i];

            if (!finishedTasksVisible && task.finished)
                continue;

            todolistHtml +=
                `<tr>
                    <td>
                        <div class="ui checkbox">
                            <input type="checkbox" ${task.finished ? "checked" : ""} onclick="TodoService.toggle('${task.key}');"/>
                            <label>${task.text}</label>
                        </div>
                    </td >
                    <td class="right aligned">
                        <a onclick="TodoService.remove('${task.key}');" >
                            <i class="red remove icon"></i>
                        </a>
                    </td>
                </tr>`;
        }
        todolist.innerHTML = todolistHtml;
    }

    scope.tryAdd = function () {
        var taskText = document.getElementById("taskText");
        if (taskText.value) {
            add(taskText.value);
            taskText.value = "";
        }
    }

    scope.remove = function (key) {
        http.get("http://localhost:5001/api/todo/remove/" + key, function (responce) {
            scope.get();
        });
    };

    scope.toggle = function (key) {
        http.get("http://localhost:5001/api/todo/task/" + key + "/toggle", function (responce) {
            scope.get();
        });
    };

    scope.get = function () {
        http.get("http://localhost:5001/api/todo/tasks", function (responce) {
            tasks = responce;
            setupTasks(tasks);
        });
    };

    scope.toggleTaskVisibility = function () {
        finishedTasksVisible = !finishedTasksVisible;
        var taskButton = document.getElementById("taskButton");
        if (finishedTasksVisible) {
            taskButton.innerText = "Hide finished tasks";
        }
        else {
            taskButton.innerText = "Show finished tasks";
        }


        setupTasks(tasks);
    };

    return scope;
} ();


window.onload = function (a) {
    TodoService.get();
    TodoService.toggleTaskVisibility();
};