// Shared quiz component. Markup contract:
//   <div class="quiz" data-answer="1">
//     <p class="q">question</p>
//     <button class="opt">option 0</button>
//     <button class="opt">option 1</button>
//     <div class="explain">why</div>
//   </div>
// Immediate feedback, no scoring, no state. Retrieval practice is the point.
document.addEventListener('click', (e) => {
    const opt = e.target.closest('.quiz button.opt');
    if (!opt) return;
    const quiz = opt.closest('.quiz');
    if (quiz.classList.contains('answered')) return;

    const options = [...quiz.querySelectorAll('button.opt')];
    const correct = options[Number(quiz.dataset.answer)];
    opt.classList.add(opt === correct ? 'right' : 'wrong');
    if (opt !== correct) correct.classList.add('right');
    quiz.classList.add('answered');
});
