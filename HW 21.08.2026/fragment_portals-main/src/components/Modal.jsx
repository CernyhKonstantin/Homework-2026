import { createPortal } from "react-dom";
import { useEffect } from "react";
import "./Modal.css";

const modalNode = document.getElementById("modal");

function Modal({ open, closeModal, title, children }) {
  useEffect(() => {
    if (!open) return undefined;

    const handleKeyDown = (event) => {
      if (event.key === "Escape") closeModal();
    };

    document.addEventListener("keydown", handleKeyDown);
    document.body.classList.add("modal-open");

    return () => {
      document.removeEventListener("keydown", handleKeyDown);
      document.body.classList.remove("modal-open");
    };
  }, [open, closeModal]);

  if (!open || !modalNode) return null;

  return createPortal(
    <div className="portal-root" role="presentation">
      <div className="background" onClick={closeModal} />
      <section
        className="modal"
        role="dialog"
        aria-modal="true"
        aria-labelledby="modal-title"
        onClick={(event) => event.stopPropagation()}
      >
        <button className="modal__close" type="button" onClick={closeModal} aria-label="Close dialog">
          &times;
        </button>
        {title && <h2 id="modal-title">{title}</h2>}
        {children}
      </section>
    </div>,
    modalNode
  );
}

export default Modal;
