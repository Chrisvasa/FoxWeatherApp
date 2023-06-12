import React from "react";
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
`;

const PopupContent = styled.div`
  background: #0086a5;
  color: #ffffff;
  padding: 2rem;
  border-radius: 0.45rem;
  font-family: Roboto, Inter, system-ui, Avenir, Helvetica, Arial, sans-serif;
  box-shadow: 0 10px 20px -15px black;
`;

const ErrorMessage = styled.p`
  font-size: 1.25rem;
`;

const ErrorPopup = ({ message, onClose }) => {
  const handleOverlayClick = (event) => {
    // Check if the clicked element is the background overlay
    if (event.target === event.currentTarget) {
      onClose();
    }
  };

  return (
    <PopupContainer onClick={handleOverlayClick}>
      <PopupContent>
        <h2>Error</h2>
        <ErrorMessage>{message}</ErrorMessage>
      </PopupContent>
    </PopupContainer>
  );
};

export default ErrorPopup;
