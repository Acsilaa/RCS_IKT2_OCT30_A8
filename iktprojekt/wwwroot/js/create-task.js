
const projectSelect = document.getElementById("projectSelect");
const taskDeadline = document.getElementById("deadlineSelect");
const deadlineHelp = document.getElementById("deadlineHelp");

function updateDeadlineLimit() {
    const selectedOption =
        projectSelect.options[projectSelect.selectedIndex];

    const projectDeadline =
        selectedOption?.dataset.deadline || "";

    if (projectDeadline) {
        taskDeadline.max = projectDeadline;

        deadlineHelp.textContent =
            `A teendő határideje legfeljebb ${projectDeadline} lehet.`;

        if (taskDeadline.value > projectDeadline) {
            taskDeadline.value = "";
        }
    } else {
        taskDeadline.removeAttribute("max");
        deadlineHelp.textContent = "";
    }
}

projectSelect.addEventListener("change", updateDeadlineLimit);
updateDeadlineLimit();
