import { CheckIcon, CloseIcon, EditIcon } from "@chakra-ui/icons";
import {
  Modal,
  ModalOverlay,
  ModalContent,
  ModalHeader,
  Divider,
  ModalCloseButton,
  ModalBody,
  List,
  ListItem,
  Flex,
  Editable,
  EditablePreview,
  EditableInput,
  ButtonGroup,
  IconButton,
  useEditableControls,
  Button,
  Box,
  FormControl,
  FormLabel,
  Input,
} from "@chakra-ui/react";
import React, { FormEvent, ReactElement } from "react";
import { Service } from "./types";

interface ServiceModalProps {
  isOpen: boolean;
  closeModal: () => void;
  services: Service[];
  save: (e: FormEvent<HTMLFormElement>) => Promise<void>;
  handleServiceChange: (value: string, index: number) => void;
  deleteService: (
    e: React.MouseEvent<HTMLButtonElement, MouseEvent>,
    serviceId: number
  ) => Promise<void>;
  handleNewServiceChange: (e: React.ChangeEvent<HTMLInputElement>) => void;
  onCreate: (e: FormEvent<HTMLFormElement>) => Promise<void>;
}

const ServiceModal = ({
  isOpen,
  closeModal,
  services,
  save,
  handleServiceChange,
  deleteService,
  handleNewServiceChange,
  onCreate,
}: ServiceModalProps): ReactElement<ServiceModalProps> => {
  const EditableControls = () => {
    const {
      isEditing,
      getSubmitButtonProps,
      getCancelButtonProps,
      getEditButtonProps,
    } = useEditableControls();
    return isEditing ? (
      <ButtonGroup size="sm" mt={3}>
        <IconButton
          aria-label="Save"
          icon={<CheckIcon />}
          {...getSubmitButtonProps()}
        >
          Save
        </IconButton>
        <IconButton
          aria-label="Close"
          icon={<CloseIcon />}
          {...getCancelButtonProps()}
        >
          Atsaukti
        </IconButton>
      </ButtonGroup>
    ) : (
      <IconButton
        ml={5}
        size="sm"
        aria-label="Edit"
        icon={<EditIcon />}
        {...getEditButtonProps()}
      >
        Edit
      </IconButton>
    );
  };
  return (
    <Modal isOpen={isOpen} onClose={closeModal}>
      <ModalOverlay />
      <ModalContent>
        <ModalHeader>
          {services.length > 0 ? "Paslaugos" : "Nauja paslauga"}
        </ModalHeader>
        <Divider />
        <ModalCloseButton />
        <ModalBody mb={5}>
          <form onSubmit={(e) => (services.length > 0 ? save(e) : onCreate(e))}>
            {services.length > 0 ? (
              <List spacing={3} m={5}>
                {services.map((service, index) => {
                  return (
                    <ListItem key={index}>
                      <Flex gap={5}>
                        <Editable
                          textAlign="left"
                          defaultValue={service.name}
                          fontSize="lg"
                          isPreviewFocusable={false}
                          onChange={(value) =>
                            handleServiceChange(value, index)
                          }
                        >
                          <EditablePreview />
                          <EditableInput />
                          <EditableControls />
                        </Editable>
                        <Button
                          colorScheme="red"
                          onClick={(e) => deleteService(e, service.id)}
                        >
                          Pašalinti
                        </Button>
                      </Flex>
                    </ListItem>
                  );
                })}
              </List>
            ) : (
              <>
                <FormControl>
                  <FormLabel>Paslauga</FormLabel>
                  <Input
                    type="text"
                    onChange={(e) => handleNewServiceChange(e)}
                  />
                </FormControl>
              </>
            )}

            <Box textAlign="center" mt={5}>
              <Button type="submit">Saugoti</Button>
            </Box>
          </form>
        </ModalBody>
      </ModalContent>
    </Modal>
  );
};

export { ServiceModal };
