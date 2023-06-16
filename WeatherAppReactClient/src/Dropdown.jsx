import React, { useState } from "react";
import styled from "styled-components";

const DropdownContainer = styled.div`
  position: relative;
  display: inline-block;
`;

const DropdownButton = styled.button`
  background: #0086a5;
  color: #fff;
  padding: 0.5rem 1rem;
  border: none;
  border-radius: 0.25rem;
  cursor: pointer;
  width: 8rem;

  &::first-letter{
    
    text-transform: capitalize;
  }

`;

const DropdownList = styled.ul`
  position: absolute;
  top: 100%;
  left: 0;
  background: #fff;
  color: #000;
  list-style: none;
  border-radius: 0.25rem;
  box-shadow: 0 10px 20px -15px black;
`;

const DropdownItem = styled.li`
  padding: 0.5rem 1rem;
  cursor: pointer;

  text-transform: capitalize;

  &:hover {
    background: #f2f2f2;
  }
`;

const Dropdown = ({ options, onSelect }) => {
  const [isOpen, setIsOpen] = useState(false);
  const [selectedOption, setSelectedOption] = useState(null);

  const handleToggle = () => {
    setIsOpen(!isOpen);
  };

  const handleSelect = (option) => {
    // setSelectedOption(option);
    onSelect(option);
    setIsOpen(false);
  };

  return (
    <DropdownContainer>
      <DropdownButton onClick={handleToggle}>
        {"Menu"}
      </DropdownButton>
      {isOpen && (
        <DropdownList>
          {options.map((option) => (
            <DropdownItem key={option} onClick={() => handleSelect(option)}>
              {option}
            </DropdownItem>
          ))}
        </DropdownList>
      )}
    </DropdownContainer>
  );
};

export default Dropdown;
