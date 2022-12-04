import {
  Modal,
  ModalOverlay,
  ModalContent,
  ModalHeader,
  Divider,
  ModalCloseButton,
  ModalBody,
  FormControl,
  FormLabel,
  Input,
  Button,
  Box,
} from "@chakra-ui/react";
import React, { ReactElement } from "react";

interface FeedbackModalProps {
  isOpen: boolean;
  closeModal: () => void;
  save: (e: React.FormEvent<HTMLFormElement>) => Promise<void>;
  handleFeedbackChange: (e:React.ChangeEvent<HTMLInputElement>) => void;
}

const FeedbackModal = ({
  isOpen,
  closeModal,
  save,
  handleFeedbackChange,
}: FeedbackModalProps): ReactElement<FeedbackModalProps> => {
  return (
    <Modal isOpen={isOpen} onClose={closeModal}>
      <ModalOverlay />
      <ModalContent>
        <ModalHeader>Atsiliepimas apie vizitą</ModalHeader>
        <Divider />
        <ModalCloseButton />
        <ModalBody>
          <form onSubmit={(e) => save(e)}>
            <FormControl>
              <FormLabel>Atsiliepimas</FormLabel>
              <Input
                type="text"
                onChange={(e) => handleFeedbackChange(e)}
              />
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

export { FeedbackModal };
