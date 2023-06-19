import PropTypes from "prop-types";
import { useEffect, useState } from "react";
import styled from "styled-components";

const PopupContainer = styled.div`
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background: rgba(0, 0, 0, 0.5);
  display: flex;
  justify-content: center;
  align-items: center;
  overflow: auto;
`;

const PopupContent = styled.ul`
  display: flex;
  flex-direction: column;
  gap: 0.25em;
  background: var(--primary);
  color: #ffffff;
  padding: 2rem;
  border-radius: 0.45rem;
  font-family: Roboto, Inter, system-ui, Avenir, Helvetica, Arial, sans-serif;
  box-shadow: 0 10px 20px -15px black;
  max-height: 50vh;
  overflow-y: auto;
`;

const ErrorMessage = styled.li`
  font-size: 1.25rem;
  list-style: none;
`;

const ErrorPopup = ({ messages, onClose, length }) => {
  const [errors, setErrors] = useState([]);

  useEffect(() => {
    const storedErrors = localStorage.getItem("errors");
    if (storedErrors) {
      setErrors(JSON.parse(storedErrors));
    }
  }, []);

  useEffect(() => {
    localStorage.setItem("errors", JSON.stringify(errors));
  }, [errors]);

  const handleOverlayClick = () => {
    clearErrors();
  };

  const clearErrors = () => {
    setErrors([]);
    localStorage.removeItem("errors");
    onClose();
  };

  return (
    <PopupContainer onClick={handleOverlayClick}>
      <PopupContent>
        <h2>{length} Errors</h2>
        {messages.map((message, index) => (
          <ErrorMessage key={index}>{message}</ErrorMessage>
        ))}
      </PopupContent>
    </PopupContainer>
  );
};

ErrorPopup.propTypes = {
  messages: PropTypes.arrayOf(PropTypes.string).isRequired,
  onClose: PropTypes.func.isRequired,
  length: PropTypes.objectOf(PropTypes.number)
};

export default ErrorPopup;
