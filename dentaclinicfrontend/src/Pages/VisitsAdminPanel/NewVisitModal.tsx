import {
  Modal,
  ModalOverlay,
  ModalContent,
  ModalHeader,
  Divider,
  ModalCloseButton,
  ModalBody,
  Box,
  Button,
  FormControl,
  FormLabel,
  Input,
  Select,
} from "@chakra-ui/react";
import moment from "moment";
import React, { FormEvent, ReactElement } from "react";
import { Service } from "./types";

interface NewVisitModalProps {
  isOpen: boolean;
  closeModal: () => void;
  save: (e: FormEvent<HTMLFormElement>) => Promise<void>;
  services: Service[];
  handleDateChange: (date: React.ChangeEvent<HTMLInputElement>) => void;
  handleTimeChange: (time: React.ChangeEvent<HTMLInputElement>) => void;
  handleServiceIdChange: (
    serviceId: React.ChangeEvent<HTMLSelectElement>
  ) => void;
  date: Date;
  time: string;
  serviceId: number | undefined;
}

const NewVisitModal = ({
  isOpen,
  closeModal,
  save,
  services,
  handleDateChange,
  handleTimeChange,
  handleServiceIdChange,
  date,
  time,
  serviceId,
}: NewVisitModalProps): ReactElement<NewVisitModalProps> => {
  return (
    <Modal isOpen={isOpen} onClose={closeModal}>
      <ModalOverlay />
      <ModalContent>
        <ModalHeader>Naujas vizitas</ModalHeader>
        <Divider />
        <ModalCloseButton />
        <ModalBody>
          <form onSubmit={(e) => save(e)}>
            <FormControl>
              <FormLabel>Data</FormLabel>
              <Input
                type="date"
                onChange={(e) => {
                  handleDateChange(e);
                }}
                value={moment(date).format("YYYY-MM-DD")}
              />
              <FormLabel>Laikas</FormLabel>
              <Input
                type="text"
                onChange={(e) => {
                  handleTimeChange(e);
                }}
                value={time}
              />
              <FormLabel>Paslauga</FormLabel>
              <Select placeholder='Pasirinkite paslaugą' onChange={(e) => handleServiceIdChange(e)}>
                {services.map((service) => {
                  return (
                    <option
                      value={service.id}
                      selected={service.id == serviceId}
                    >
                      {service.name}
                    </option>
                  );
                })}
              </Select>
            </FormControl>
            <Box textAlign="center">
              <Button m={5} type="submit">
                Išsaugoti
              </Button>
            </Box>
          </form>
        </ModalBody>
      </ModalContent>
    </Modal>
  );
};

export { NewVisitModal };
